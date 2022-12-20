using AutoMapper;
using PinPoint.Data.Domain;
using PinPoint.Infrastructure.MapperService.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinPoint.Infrastructure.MapperService.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<GetUserDTO, User>();
            CreateMap<User, GetUserDTO>();

            CreateMap<PostUserDTO, User>();
            //.ForMember(
            //    dest => dest.user_id,
            //    opt => opt.MapFrom(src => $"{new Guid()}")
            //);
            CreateMap<User, PostUserDTO>();

            CreateMap<PutUserDTO, User>();
            CreateMap<User, PutUserDTO>();
        }
    }
}
