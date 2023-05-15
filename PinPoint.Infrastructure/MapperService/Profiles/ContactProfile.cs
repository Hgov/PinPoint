using AutoMapper;
using PinPoint.Data.Domain;
using PinPoint.Infrastructure.MapperService.Models;

namespace PinPoint.Infrastructure.MapperService.Profiles
{
    public class SingleObjectToListConverter<T> : ITypeConverter<T, List<T>>
    {
        public List<T> Convert(T source, List<T> destination, ResolutionContext context)
        {
            return new List<T>() { source };
        }
    }
    public class IEnumerableToListConverter<T> : ITypeConverter<IEnumerable<T>, List<T>>
    {
        public List<T> Convert(IEnumerable<T> source, List<T> destination, ResolutionContext context)
        {
            destination = source.ToList();
            return destination;
        }
    }


    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            AllowNullCollections = true;
            
            #region Get Map
            CreateMap<GetContactDTO, Contact>();
            CreateMap<Contact, GetContactDTO>();

            CreateMap<Contact, List<GetContactDTO>>();
            CreateMap<List<GetContactDTO>, Contact>();

            CreateMap<GetContactDTO, List<GetContactDTO>>().ConvertUsing<SingleObjectToListConverter<GetContactDTO>>();

            #endregion

            #region Post Map
            CreateMap<PostContactDTO, Contact>()
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
            CreateMap<Contact, PostContactDTO>();

            CreateMap<IEnumerable<PostContactDTO>, List<PostContactDTO>>().ConvertUsing<IEnumerableToListConverter<PostContactDTO>>(); 
            #endregion

            #region Put Map
            CreateMap<PutContactDTO, Contact>()
                .ForMember(
                dest => dest.last_updated_tsz,
                opt => opt.MapFrom(src => $"{DateTime.Now}")
            );
            CreateMap<Contact, PutContactDTO>();
            #endregion

            #region Delete Map
            //CreateMap<DeleteContactDTO, Contact>();
            //CreateMap<Contact, DeleteContactDTO>()
            //    .ForMember(
            //    dest => dest.contact.delete_tsz,
            //    opt => opt.MapFrom(src => $"{DateTime.Now}")
            //);
            #endregion

        }
    }
}
