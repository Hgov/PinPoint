using AutoMapper;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PinPoint.Data.Domain;
using PinPoint.Infrastructure.MapperService.Models.User;
using PinPoint.Infrastructure.Response;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinPoint.Infrastructure.MapperService.Profiles
{
    public class SingleObjectToListConverter<T> : ITypeConverter<T, List<T>>
    {
        public List<T> Convert(T source, List<T> destination, ResolutionContext context)
        {
            return new List<T>() { source };
        }
    }

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            #region Get Map
            CreateMap<GetUserDTO, User>();
            CreateMap<User, GetUserDTO>();

            CreateMap<User, List<GetUserDTO>>();
            CreateMap<List<GetUserDTO>, User>();

            CreateMap<GetUserDTO, List<GetUserDTO>>().ConvertUsing<SingleObjectToListConverter<GetUserDTO>>();
            #endregion

            #region Post Map
            CreateMap<PostUserDTO, User>()
              .ForMember(
                  dest => dest.creation_tsz,
                  opt => opt.MapFrom(src => $"{DateTime.Now}")
              ).
              ForMember(
                  dest => dest.status_active,
              opt => opt.MapFrom(src => $"{true}")
              ).
              ForMember(
                  dest => dest.status_visibility,
              opt => opt.MapFrom(src => $"{true}")
              );
            CreateMap<User, PostUserDTO>();
            #endregion

            #region Put Map
            CreateMap<PutUserDTO, User>()
                .ForMember(
                dest => dest.last_updated_tsz,
                opt => opt.MapFrom(src => $"{DateTime.Now}")
            );
            CreateMap<User, PutUserDTO>();
            #endregion

        }
    }
}
