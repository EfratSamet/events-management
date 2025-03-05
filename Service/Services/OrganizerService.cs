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
    public class OrganizerService: IService<OrganizerDto>
    {

        private readonly IOrganizerRepository _repository;
        private readonly IMapper _mapper;

        public OrganizerService(IOrganizerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public OrganizerDto Add(OrganizerDto item)
        {
            return _mapper.Map<OrganizerDto>(_repository.Add(_mapper.Map<Organizer>(item)));
        }

        public void Delete(string id)
        {
            _repository.Delete(id);
        }

        public OrganizerDto Get(string id)
        {
            return _mapper.Map<OrganizerDto>(_repository.Get(id));
        }

        public List<OrganizerDto> GetAll()
        {
            return _mapper.Map<List<OrganizerDto>>(_repository.GetAll());
        }

        public OrganizerDto Update(string id, OrganizerDto item)
        {
            return _mapper.Map<OrganizerDto>(_repository.Update(id, _mapper.Map<Organizer>(item)));
        }
        public List<EventDto> GetEventsByOrganizerId(string id)
        {
            var events = _repository.GetEventsByOrganizerId(id);
            return _mapper.Map<List<EventDto>>(events);
        }

        public List<GroupDto> GetGroupsByOrganizerId(string id)
        {
            var groups = _repository.GetGroupsByOrganizerId(id);
            return _mapper.Map<List<GroupDto>>(groups);
        }
    }
}
