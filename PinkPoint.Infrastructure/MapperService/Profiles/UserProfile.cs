using AutoMapper;
using PinkPoint.Data.Domain;
using PinkPoint.Infrastructure.MapperService.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkPoint.Infrastructure.MapperService.Profiles
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
