using AutoMapper;
using HRM.Domain.Models;
using HRM.Domain.Models.Identity;
using HRM.Infra.CrossCutting.Identity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRM.Infra.CrossCutting.Identity.Mapping
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<User, AppUser>().ConstructUsing(u => new AppUser { UserName = u.UserName, Email = u.Email }).ForMember(au => au.Id, opt => opt.Ignore());
            CreateMap<AppUser, User>().ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email)).
                                       ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.PasswordHash)).
                                       ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}
