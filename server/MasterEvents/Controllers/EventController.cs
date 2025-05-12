using Microsoft.AspNetCore.Mvc;
using Service.Dtos;
using Service.Interfaces;
using System.Collections.Generic;

namespace MasterEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        // GET: api/Event
        [HttpGet]
        public List<EventDto> Get()
        {
            return _eventService.GetAll();
        }

        // GET api/Event/5
        [HttpGet("{id}")]
        public EventDto Get(int id)
        {
            return _eventService.Get(id);
        }

        [HttpGet("organizer/{organizerId}")]
        public ActionResult<List<EventDto>> GetByOrganizerId(int organizerId)
        {
            var events = _eventService.GetAll().FindAll(e => e.organizerId == organizerId);
            return Ok(events); 
        }


        // POST api/Event
        [HttpPost]
        public void Post([FromBody] EventDto value)
        {
            _eventService.Add(value);
        }

        // PUT api/Event/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] EventDto value)
        {
            _eventService.Update(id, value);
        }

        // DELETE api/Event/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _eventService.Delete(id);
        }
       
        }



    
}
