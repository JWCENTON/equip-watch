﻿using AutoMapper;
using DTO.CommissionDTOs;

namespace DTO.Mappers;

public class CommissionMappingProfile : Profile
{
    public CommissionMappingProfile()
    {
        CreateMap<Domain.Commission.Models.Commission.Commission, CreateCommissionDTO>()
            .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
            .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client))
            .ReverseMap();

        CreateMap<Domain.Commission.Models.Commission.Commission, FullCommissionDTO>()
            .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
            .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client))
            .ReverseMap();

        CreateMap<Domain.Commission.Models.Commission.Commission, UpdateCommissionDTO>()
            .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client))
            .ReverseMap();
    }
}