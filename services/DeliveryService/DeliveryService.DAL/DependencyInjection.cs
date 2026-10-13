using System.Text.Json;
using DeliveryService.BLL.Services.Interfaces;
using DeliveryService.DAL.Integrations.Redis;
using DeliveryService.DAL.Integrations.Stripe;
using DeliveryService.DAL.Outbox;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Microsoft.Extensions.Options;
using Polly;
using Quartz;
using StackExchange.Redis;

namespace DeliveryService.DAL;

public static class DependencyInjection
{

    public static IServiceCollection AddDal(this IServiceCollection services)
    {
        services.AddOptions<OutboxOptions>()
            .BindConfiguration(OutboxOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddKeyedSingleton<JsonSerializerOptions>(
            OutboxOptions.JsonOptionsKey,
            new JsonSerializerOptions(JsonSerializerDefaults.Web));

        services.AddStripePaymentGateway();
        services.AddRedisCourierLocationStore();
        services.AddScheduling();

        return services;
    }

    public static IServiceCollection AddRedisCourierLocationStore(this IServiceCollection services)
    {
        services.AddOptions<RedisOptions>()
            .BindConfiguration(RedisOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton<IConnectionMultiplexer>(serviceProvider =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<RedisOptions>>().Value;
            return ConnectionMultiplexer.Connect(options.ConnectionString);
        });

        services.AddSingleton<ICourierLocationStore, RedisCourierLocationStore>();

        return services;
    }

    public static IServiceCollection AddStripePaymentGateway(this IServiceCollection services)
    {
        services.AddOptions<StripeOptions>()
            .BindConfiguration(StripeOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddKeyedSingleton<JsonSerializerOptions>(
            StripeOptions.JsonOptionsKey,
            new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            });

        services.AddHttpClient<IPaymentGateway, StripePaymentGateway>((serviceProvider, client) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<StripeOptions>>().Value;

                client.BaseAddress = new Uri(options.BaseUrl);
                client.DefaultRequestHeaders.Authorization = new("Bearer", options.SecretKey);

                client.Timeout = Timeout.InfiniteTimeSpan;
            })
            .AddStandardResilienceHandler()
            .Configure((options, serviceProvider) =>
            {
                var stripeOptions = serviceProvider.GetRequiredService<IOptions<StripeOptions>>().Value;

                options.Retry.MaxRetryAttempts = stripeOptions.RetryCount;
                options.Retry.Delay = TimeSpan.FromSeconds(stripeOptions.RetryDelayInSeconds);
                options.Retry.BackoffType = DelayBackoffType.Exponential;
                options.Retry.UseJitter = true;

                options.AttemptTimeout.Timeout = TimeSpan.FromSeconds(stripeOptions.AttemptTimeoutInSeconds);
                options.TotalRequestTimeout.Timeout = TimeSpan.FromSeconds(stripeOptions.TotalTimeoutInSeconds);

                options.CircuitBreaker.SamplingDuration =
                    TimeSpan.FromSeconds(stripeOptions.CircuitBreakerSamplingDurationInSeconds);
            });

        return services;
    }
    
    public static IServiceCollection AddScheduling(this IServiceCollection services)
    {
        services.AddQuartz(quartz =>
        {
            quartz.AddJob<OutboxProcessorJob>(job => job.WithIdentity(OutboxProcessorJob.Key));

            quartz.AddTrigger((serviceProvider, trigger) =>
            {
                var outboxOptions = serviceProvider.GetRequiredService<IOptions<OutboxOptions>>().Value;

                trigger
                    .ForJob(OutboxProcessorJob.Key)
                    .StartNow()
                    .WithSimpleSchedule(schedule => schedule
                        .WithInterval(TimeSpan.FromSeconds(outboxOptions.IntervalInSeconds))
                        .RepeatForever()
                        .WithMisfireInstruction(SimpleTriggerMisfireInstruction.NextWithRemainingCount));
            });
        });

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        return services;
    }
}
