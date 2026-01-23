using AuthService.Application.DTO.Customers;
using AuthService.Application.DTO.Users;
using AuthService.Domain.Entities;
using AutoMapper;

namespace AuthService.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserAccountDto>();
            CreateMap<Customer, CustomerDto>();
        }
    }
}
