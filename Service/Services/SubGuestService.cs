using AutoMapper;
using Repository.Entity;
using Repository.Interfaces;
using Service.Dtos;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class SubGuestService:IService<SubGuestDto>
    {
        private readonly IRepository<SubGuest> _repository;
        private readonly IMapper _mapper;

        public SubGuestService(IRepository<SubGuest> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public SubGuestDto Add(SubGuestDto item)
        {
            return _mapper.Map<SubGuestDto>(_repository.Add(_mapper.Map<SubGuest>(item)));
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public SubGuestDto Get(int id)
        {
            return _mapper.Map<SubGuestDto>(_repository.Get(id));
        }

        public List<SubGuestDto> GetAll()
        {
            return _mapper.Map<List<SubGuestDto>>(_repository.GetAll());
        }

        public SubGuestDto Update(int id, SubGuestDto item)
        {
            return _mapper.Map<SubGuestDto>(_repository.Update(id, _mapper.Map<SubGuest>(item)));
        }
    }
}
