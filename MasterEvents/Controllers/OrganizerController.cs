using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizerController : ControllerBase
    {
        private readonly IService<OrganizerDto> _organizerService;
        public OrganizerController(IService<OrganizerDto> organizerService)
        {
            _organizerService = organizerService;
        }

        // GET: api/<OrganizerController>
        [HttpGet]
        public List<OrganizerDto> Get()
        {
            return _organizerService.GetAll();
        }

        // GET api/<OrganizerController>/5
        [HttpGet("{id}")]
        public OrganizerDto Get(string id)
        {
            return _organizerService.Get(id);
        }

        // POST api/<OrganizerController>
        [HttpPost]
        public IActionResult Post([FromBody] OrganizerDto value)
        {
            if (value == null)
            {
                return BadRequest("Invalid data.");
            }

            // הוספת המארגן
            _organizerService.Add(value);

            // החזרת תשובת הצלחה עם סטטוס 201 (נוצר)
            return CreatedAtAction(nameof(Get), new { id = value.id }, value);
        }

        // PUT api/<OrganizerController>/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] OrganizerDto value)
        {
            _organizerService.Update(id, value);    
        }

        // DELETE api/<OrganizerController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _organizerService.Delete(id);
        }
        // GET api/Organizer/{id}/events - קבלת רשימת האירועים של המארגן
        //[HttpGet("{id}/events")]
        //public ActionResult<List<EventDto>> GetEventsByOrganizerId(string id)
        //{
        //    var events = _organizerService.GetEventsByOrganizerId(id);
        //    if (events == null || events.Count == 0)
        //        return NotFound("No events found for this organizer.");
        //    return events;
        //}

        //// GET api/Organizer/{id}/groups - קבלת רשימת הקבוצות של המארגן
        //[HttpGet("{id}/groups")]
        //public ActionResult<List<GroupDto>> GetGroupsByOrganizerId(string id)
        //{
        //    var groups = _organizerService.GetGroupsByOrganizerId(id);
        //    if (groups == null || groups.Count == 0)
        //        return NotFound("No groups found for this organizer.");
        //    return groups;
        //}

    }
}
