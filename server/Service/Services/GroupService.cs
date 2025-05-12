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
    public class GroupService:IGroupService
    {

        private readonly IGroupRepository _repository;
        private readonly IMapper _mapper;

        public GroupService(IGroupRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public GroupDto Add(GroupDto item)
        {
            return _mapper.Map<GroupDto>(_repository.Add(_mapper.Map<Group>(item)));
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public GroupDto Get(int id)
        {
            return _mapper.Map<GroupDto>(_repository.Get(id));
        }

        public List<GroupDto> GetAll()
        {
            return _mapper.Map<List<GroupDto>>(_repository.GetAll());
        }

        public GroupDto Update(int id, GroupDto item)
        {
            return _mapper.Map<GroupDto>(_repository.Update(id, _mapper.Map<Group>(item)));
        }
        public List<GroupDto> GetGroupsByOrganizerId(int organizerId)
        {
            return _mapper.Map<List<GroupDto>>(_repository.GetGroupsByOrganizerId(organizerId));
        }
    }
}
