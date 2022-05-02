using AuthApi.UserRequest;
using AuthApiFluentValidation.Models;
using AuthBusinessLayer.Requests;
using AutoMapper;

namespace AuthApi
{
    public sealed class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserRequestDto, UserRequestValidation>();
            CreateMap<UserRequestDto, RequestFromAuthApiDto>();
        }
    }
}