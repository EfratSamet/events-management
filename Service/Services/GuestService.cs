using AutoMapper;
using Repository.Entity;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Dtos;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class GuestService:IGuestService
    {
        private readonly IGuestRepository _repository;
        private readonly IMapper _mapper;

        public GuestService(IGuestRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public GuestDto Add(GuestDto item)
        {
            return _mapper.Map<GuestDto>(_repository.Add(_mapper.Map<Guest>(item)));
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public GuestDto Get(int id)
        {
            return _mapper.Map<GuestDto>(_repository.Get(id));
        }

        public List<GuestDto> GetAll()
        {
            return _mapper.Map<List<GuestDto>>(_repository.GetAll());
        }

        public GuestDto Update(int id, GuestDto item)
        {
            return _mapper.Map<GuestDto>(_repository.Update(id, _mapper.Map<Guest>(item)));
        }
        public void SendEmails(int eventId, string subject, string body)
        {
            _repository.SendEmails(eventId, subject, body);

        }
        public List<GuestDto> GetGuestsByGroup(int groupId)
        {
            return _mapper.Map<List<GuestDto>>(_repository.GetGuestsByGroup(groupId));
        }
        public List<GuestDto> GetGuestsByEventId(int eventId)
        {
            return _mapper.Map<List<GuestDto>>(_repository.GetGuestsByEventId(eventId));
        }
        public List<GuestDto> GetGuestsByOrganizerId(int organizerId)
        {
            var guests = _repository.GetGuestsByOrganizerId(organizerId);
            return _mapper.Map<List<GuestDto>>(guests);
        }
    }
}
