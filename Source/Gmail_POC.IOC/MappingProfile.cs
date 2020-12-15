using AutoMapper;
using Gmail_POC.Data.Models;

namespace Gmail_POC.IoC
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add mappings over here
            CreateMap<Users, UserModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName))
                .ReverseMap();

            CreateMap<UserEvent, UserModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.OrgDisplayName))
                .ReverseMap();

            CreateMap<EventAttendee, UserModel>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<UserRecurringEvent, UserModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RecurringEventId))
                .ReverseMap();
        }
    }
}