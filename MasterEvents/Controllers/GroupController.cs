using Microsoft.AspNetCore.Mvc;
using Repository.Entity;
using Service.Dtos;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterEvents.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        // GET: api/<GroupController>
        [HttpGet]
        public List<GroupDto> Get()
        {
            return _groupService.GetAll();
        }

        // GET api/<GroupController>/5
        [HttpGet("{id}")]
        public GroupDto Get(int id)
        {
            return _groupService.Get(id);
        }

        // POST api/<GroupController>
        [HttpPost]
        public void Post([FromBody] GroupDto value)
        {
            _groupService.Add(value);   
        }

        // PUT api/<GroupController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] GroupDto value)
        {
            _groupService.Update(id, value);
        }

        // DELETE api/<GroupController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _groupService.Delete(id);
        }
        [HttpGet("organizer/{organizerId}")]
        public IActionResult GetGroupsByOrganizerId(int organizerId)
        {
            var groups =_groupService.GetGroupsByOrganizerId(organizerId);
            if (groups == null || !groups.Any())
            {
                return NotFound("No guests found for this event.");
            }
            return Ok(groups);
        }
    }
}
