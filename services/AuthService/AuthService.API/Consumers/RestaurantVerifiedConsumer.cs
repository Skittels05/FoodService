using MassTransit;
using MediatR;
using RestaurantService.BLL.Events; 
using AuthService.Application.CQRS.RestaurantManagers.Commands;

namespace AuthService.API.Consumers;

public class RestaurantVerifiedConsumer(
    IMediator mediator, 
    ILogger<RestaurantVerifiedConsumer> logger) : IConsumer<RestaurantVerifiedEvent>
{
    public async Task Consume(ConsumeContext<RestaurantVerifiedEvent> context)
    {
        var restaurantId = context.Message.RestaurantId;
        
        logger.LogInformation("Received verification event for Restaurant ID: {RestaurantId}", restaurantId);

        var command = new VerifyManagerByRestaurantCommand(restaurantId);
        await mediator.Send(command);
        
        logger.LogInformation("Manager for Restaurant ID: {RestaurantId} has been successfully verified and synced with Auth0.", restaurantId);
    }
}
