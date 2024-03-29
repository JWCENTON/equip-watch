﻿using AutoMapper;
using Domain.User.Models;
using DTO.UserDTOs;

namespace DTO.Mappers;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<Domain.User.Models.User, CreateUserDTO>().ReverseMap();
        CreateMap<Domain.User.Models.User, FullUserDTO>().ReverseMap();
        CreateMap<Domain.User.Models.User, LoginUserDTO>().ReverseMap();
        CreateMap<Domain.User.Models.User, PartialUserDTO>().ReverseMap();

        CreateMap<Domain.User.Models.User, UpdateUserDTO>();


        CreateMap<UpdateUserDTO, Domain.User.Models.User>()
            .ForAllMembers(opt => opt
                .Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Domain.User.Models.User, ForgotPasswordDTO>().ReverseMap();

        CreateMap<ResetPasswordDTO, Domain.User.Models.User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.PasswordResetToken, opt => opt.MapFrom(src => src.Token))
            .ReverseMap();
    }
}