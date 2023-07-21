﻿using AutoMapper;
using DTO.CheckOutDTOs;

namespace DTO.Mappers;

public class CheckOutMappingProfile : Profile
{
    public CheckOutMappingProfile()
    {
        CreateMap<Domain.CheckOut.Models.CheckOut, CreateCheckOutDTO>()
            .ForMember(dest => dest.Equipment, opt => opt.MapFrom(src => src.Equipment))
            .ForMember(dest => dest.Employ, opt => opt.MapFrom(src => src.Employee))
            .ReverseMap();

        CreateMap<Domain.CheckOut.Models.CheckOut, FullCheckOutDTO>()
            .ForMember(dest => dest.Equipment, opt => opt.MapFrom(src => src.Equipment))
            .ForMember(dest => dest.Employ, opt => opt.MapFrom(src => src.Employee))
            .ReverseMap();

        CreateMap<Domain.CheckOut.Models.CheckOut, UpdateCheckOutDTO>()
            .ForMember(dest => dest.Employ, opt => opt.MapFrom(src => src.Employee))
            .ReverseMap();
    }
}