using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class OrganizerService: IOrganizerService
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
            if (_repository.ExistsByEmail(item.mail))
            {
                throw new ArgumentException("Email already exists."); // במקום חריגה כללית
            }
            return _mapper.Map<OrganizerDto>(_repository.Add(_mapper.Map<Organizer>(item)));

        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public OrganizerDto Get(int id)
        {
            return _mapper.Map<OrganizerDto>(_repository.Get(id));
        }

        public List<OrganizerDto> GetAll()
        {
            return _mapper.Map<List<OrganizerDto>>(_repository.GetAll());
        }

        public OrganizerDto Update(int id, OrganizerDto item)
        {
            return _mapper.Map<OrganizerDto>(_repository.Update(id, _mapper.Map<Organizer>(item)));
        }
     
    }
}
