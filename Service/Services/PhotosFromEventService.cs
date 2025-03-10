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
    public class PhotosFromEventService:IService<PhotosFromEventDto>
    {
        private readonly IRepository<PhotosFromEvent> _repository;
        private readonly IMapper _mapper;

        public PhotosFromEventService(IRepository<PhotosFromEvent> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public PhotosFromEventDto Add(PhotosFromEventDto item)
        {
            return _mapper.Map<PhotosFromEventDto>(_repository.Add(_mapper.Map<PhotosFromEvent>(item)));
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public PhotosFromEventDto Get(int id)
        {
            return _mapper.Map<PhotosFromEventDto>(_repository.Get(id));
        }

        public List<PhotosFromEventDto> GetAll()
        {
            return _mapper.Map<List<PhotosFromEventDto>>(_repository.GetAll());
        }

        public PhotosFromEventDto Update(int id, PhotosFromEventDto item)
        {
            return _mapper.Map<PhotosFromEventDto>(_repository.Update(id, _mapper.Map<PhotosFromEvent>(item)));
        }
    }
}

