using AutoMapper;
using WebApiDataLayer.Models;
using WebApi.Requests;
using WebApiBusinessLayer.Requests;
using WebApiBusinessLayer.Responses;
using WebApiFluentValidation.Models;

namespace WebApi
{
    public sealed class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ClinicRequestDto, ClinicRequestValidation>();
            CreateMap<KittenRequestDto, KittenRequestValidation>();
            CreateMap<ClinicServiceRequestDto, ClinicServiceRequestValidation>();

            CreateMap<ClinicRequestDto, RequestClinicsFromWebApiDto>();
            CreateMap<ClinicServiceRequestDto, RequestClinicServiceFromWebApiDto>();
            CreateMap<KittenRequestDto, RequestKittensFromWebApiDto>();

            CreateMap<Kitten, KittenDTO>();
            CreateMap<Clinic, ClinicDTO>();
        }
    }
}