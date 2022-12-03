using AutoMapper;
using PinkPoint.Data.Domain;
using PinkPoint.Mapper.Models.User;

namespace PinkPoint.Mapper.Mapper.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<GetUserDTO, User>();
            CreateMap<User, GetUserDTO>();


            CreateMap<PostUserDTO, User>();
            CreateMap<User, PostUserDTO>();
        }
    }
}
