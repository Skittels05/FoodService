using AuthService.Application.CQRS.Couriers.Commands;
using AuthService.Application.CQRS.Customers.Commands;
using AuthService.Application.DTO.Courier;
using AuthService.Application.DTO.Customers;
using AuthService.Application.DTO.Users;
using AuthService.Domain.Entities;
using AutoMapper;

namespace AuthService.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserAccountDto>();

        CreateMap<Customer, CustomerDto>();
        CreateMap<CreateCustomerCommand, Customer>();

        CreateMap<Courier, CourierDto>();
        CreateMap<CreateCourierCommand, Courier>();
    }
}
