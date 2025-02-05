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
    public class SeatingService:IService<SeatingDto>
    {
        private readonly IRepository<Seating> _repository;
        private readonly IMapper _mapper;

        public SeatingService(IRepository<Seating> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public SeatingDto Add(SeatingDto item)
        {
            return _mapper.Map<SeatingDto>(_repository.Add(_mapper.Map<Seating>(item)));
        }

        public void Delete(string id)
        {
            _repository.Delete(id);
        }

        public SeatingDto Get(string id)
        {
            return _mapper.Map<SeatingDto>(_repository.Get(id));
        }

        public List<SeatingDto> GetAll()
        {
            return _mapper.Map<List<SeatingDto>>(_repository.GetAll());
        }

        public SeatingDto Update(string id, SeatingDto item)
        {
            return _mapper.Map<SeatingDto>(_repository.Update(id, _mapper.Map<Seating>(item)));
        }
    }
}
