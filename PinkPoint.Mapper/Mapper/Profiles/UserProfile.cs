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
