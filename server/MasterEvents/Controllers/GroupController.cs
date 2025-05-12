using Microsoft.AspNetCore.Mvc;
using Repository.Entity;
using Service.Dtos;
using Service.Interfaces;

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

        // GET api/group/organizer/{organizerId}
        [HttpGet("organizer/{organizerId}")]
        public IActionResult GetGroupsByOrganizerId(int organizerId)
        {
            var groups = _groupService.GetGroupsByOrganizerId(organizerId);
            if (groups == null || !groups.Any())
            {
                return NotFound("No groups found for this organizer.");
            }
            return Ok(groups);
        }

        // GET api/group/organizer/{organizerId}/name/{name}
        [HttpGet("organizer/{organizerId}/name/{name}")]
        public IActionResult GetGroupByName(int organizerId, string name)

        {
            // שליפת כל הקבוצות לפי מזהה המארגן
            var groups = _groupService.GetGroupsByOrganizerId(organizerId);

            // חיפוש קבוצה לפי שם
            var group = groups.FirstOrDefault(g => g.name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (group != null)
            {
                return Ok(group.id); // מחזירים רק את המזהה של הקבוצה
            }

            return NotFound($"לא נמצא מזהה עבור קבוצה בשם {name} עם מזהה מארגן {organizerId}");
        }

    }
}
