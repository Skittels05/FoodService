using AuthService.Application.DTO.Customers;
using AuthService.Application.DTO.Users;
using AuthService.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

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
