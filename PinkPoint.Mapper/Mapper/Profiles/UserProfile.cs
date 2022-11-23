using AutoMapper;
using PinkPoint.Data.Domain;
using PinkPoint.Mapper.Models.User;

namespace PinkPoint.Mapper.Mapper.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<GetUserDTO, User>()
                .ForMember(
                    dest => dest.user_id,
                    opt => opt.MapFrom(src => $"{src.user_id}")
                )
                .ForMember(
                    dest => dest.first_name,
                    opt => opt.MapFrom(src => $"{src.first_name}")
                )
                .ForMember(
                    dest => dest.last_name,
                    opt => opt.MapFrom(src => $"{src.last_name}")
                )
                .ForMember(
                    dest => dest.email,
                    opt => opt.MapFrom(src => $"{src.email}")
                )
                .ForMember(
                    dest => Convert.ToDateTime(dest.birth_date),
                    opt => opt.MapFrom(src => $"{src.birth_date}")
                )
                .ForMember(
                    dest => dest.phone,
                    opt => opt.MapFrom(src => $"{src.phone}")
                )
                .ForMember(
                    dest => dest.bio,
                    opt => opt.MapFrom(src => $"{src.bio}")
                )
                .ForMember(
                    dest => dest.gender,
                    opt => opt.MapFrom(src => $"{src.gender}")
                )
                .ForMember(
                    dest => dest.creation_tsz,
                    opt => opt.MapFrom(src => $"{src.creation_tsz}")
                )
                .ForMember(
                    dest => dest.last_updated_tsz,
                    opt => opt.MapFrom(src => $"{src.last_updated_tsz}")
                )
                .ForMember(
                    dest => dest.delete_tsz,
                    opt => opt.MapFrom(src => $"{src.delete_tsz}")
                )
                .ForMember(
                    dest => dest.status_active,
                    opt => opt.MapFrom(src => $"{src.status_active}")
                )
                .ForMember(
                    dest => dest.status_visibility,
                    opt => opt.MapFrom(src => $"{src.status_visibility}")
                );
        }
    }
}
