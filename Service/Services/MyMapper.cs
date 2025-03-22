using AutoMapper;
using Repository.Entity;
using Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class MyMapper : Profile
    {
        public MyMapper()
        {
            CreateMap<PhotosFromEvent, PhotosFromEventDto>()
                .ForMember(dest => dest.Image, src => src.MapFrom(s => convertToByte(Environment.CurrentDirectory + "/Images/" + s.imageUrl)));

            CreateMap<PhotosFromEventDto, PhotosFromEvent>().ForMember(dest => dest.imageUrl, src => src.MapFrom(s => s.File.FileName));

            CreateMap<Event, EventDto>().ReverseMap();
            CreateMap<Group, GroupDto>().ReverseMap();
            CreateMap<Guest, GuestDto>().ReverseMap();
            CreateMap<GuestInEvent, GuestInEventDto>().ReverseMap();
            CreateMap<Organizer, OrganizerDto>().ReverseMap();
            CreateMap<Seating, SeatingDto>().ReverseMap();
            CreateMap<SubGuest, SubGuestDto>().ReverseMap();

            // הוספת המיפוי החסר
            CreateMap<SubGuest, GuestInEventDto>()
                .ForMember(dest => dest.guestId, opt => opt.MapFrom(src => src.id));  // כאן תוכל להוסיף שדות נוספים אם יש צורך
        }

        public byte[] convertToByte(string image)
        {
            var res = System.IO.File.ReadAllBytes(image);
            return res;
        }
    }
}
