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
    public class SubGuestService : ISubGuestService
    {
        private readonly ISubGuestRepository _subGuestRepository;
        private readonly IGuestInEventRepository guestInEventRepository;
        private readonly IMapper _mapper;

        public SubGuestService(ISubGuestRepository subGuestRepository, IGuestInEventRepository _guestInEventRepository, IMapper mapper)
        {
            _subGuestRepository = subGuestRepository;
            guestInEventRepository = _guestInEventRepository;
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
        public List<SubGuestDto> GetSubGuestsByGuestId(int guestId)
        {
            return _mapper.Map<List<SubGuestDto>>(_subGuestRepository.GetSubGuestsByGuestId(guestId));
        }
        public List<SubGuestDto> GetSubGuestByGuestUdAndEventId(int guestId, int eventId)
        {
            return _mapper.Map<List<SubGuestDto>>(_subGuestRepository.GetSubGuestsByEventIdAndGuestId(guestId, eventId));
        }

    }
}
