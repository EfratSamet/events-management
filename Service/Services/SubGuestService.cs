using AutoMapper;
using Repository.Entity;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Dtos;
using Service.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Service.Services
{
    public class SubGuestService :  ISubGuestService
    {
        private readonly ISubGuestRepository _subGuestRepository;
        private readonly IGuestRepository _guestRepository;
        private readonly IMapper _mapper;

        public SubGuestService(ISubGuestRepository subGuestRepository, IGuestRepository guestRepository, IMapper mapper)
        {
            _subGuestRepository = subGuestRepository;
            _guestRepository = guestRepository;
            _mapper = mapper;
        }

        // הוספת תת-אורח חדש
        public SubGuestDto Add(SubGuestDto item)
        {
            var entity = _mapper.Map<SubGuest>(item);
            var addedEntity = _subGuestRepository.Add(entity);
            return _mapper.Map<SubGuestDto>(addedEntity);
        }

        // מחיקת תת-אורח
        public void Delete(int id)
        {
            _subGuestRepository.Delete(id);
        }

        // קבלת תת-אורח לפי ID
        public SubGuestDto Get(int id)
        {
            return _mapper.Map<SubGuestDto>(_subGuestRepository.Get(id));
        }

        // קבלת כל תתי האורחים
        public List<SubGuestDto> GetAll()
        {
            return _mapper.Map<List<SubGuestDto>>(_subGuestRepository.GetAll());
        }

        // עדכון תת-אורח
        public SubGuestDto Update(int id, SubGuestDto item)
        {
            var entity = _mapper.Map<SubGuest>(item);
            var updatedEntity = _subGuestRepository.Update(id, entity);
            return _mapper.Map<SubGuestDto>(updatedEntity);
        }

        // פונקציה לסידור מקומות הישיבה
        public List<SeatingDto> ArrangeSeating(int eventId)
        {
            var allGuests = _guestRepository.GetGuestsByEventId(eventId);
            var seatings = new List<SeatingDto>();
            int tableNumber = 1;
            int seatNumber = 1;
            int maxSeatsPerTable = 10;

            foreach (var guest in allGuests)
            {
                var subGuests = _subGuestRepository.GetSubGuestsByGuestId(guest.id);
                var group = new List<int> { guest.id };
                group.AddRange(subGuests.Select(sg => sg.id));

                foreach (var guestOrSubGuestId in group)
                {
                    seatings.Add(new SeatingDto
                    {
                        eventId = eventId,
                        subGuestId = guestOrSubGuestId,
                        table = tableNumber,
                        seat = seatNumber++
                    });

                    if (seatNumber > maxSeatsPerTable)
                    {
                        seatNumber = 1;
                        tableNumber++;
                    }
                }
            }

            return seatings;
        }
    }
}
