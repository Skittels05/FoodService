using AuthService.Application.CQRS.Couriers.Commands;
using AuthService.Application.CQRS.Customers.Commands;
using AuthService.Application.CQRS.Users.Commands;
using AuthService.Application.DTO.Courier;
using AuthService.Application.DTO.Customers;
using AuthService.Application.DTO.Users;
using AuthService.Domain.Common;
using AuthService.Domain.Entities;
using AutoMapper;

namespace AuthService.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        CreateMap(typeof(PagedList<>), typeof(PagedList<>))
            .ConvertUsing(typeof(PagedListConverter<,>));

        CreateMap<User, UserAccountDto>();
        CreateMap<CreateUserCommand, User>()
            .ConstructUsing(src => new User(src.Email, src.UserName, src.Role));
        CreateMap<UpdateUserCommand, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Courier, CourierDto>();
        CreateMap<CreateCourierCommand, Courier>()
            .ConstructUsing(src => new Courier(
                src.UserId,
                src.Name,
                src.VehicleType,
                src.DocumentsPath,
                src.PhotoVerificationPath
            ));

        CreateMap<Customer, CustomerDto>();
        CreateMap<CreateCustomerCommand, Customer>()
            .ConstructUsing(src => new Customer(src.UserId, src.Name));
    }
}
