using Microsoft.AspNetCore.Mvc;
using Service.Dtos;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IService<EventDto> _eventService;  
        public EventController(IService<EventDto> eventService)
        {
            _eventService = eventService;
        }

        // GET: api/<EventController>
        [HttpGet]
        public List<EventDto> Get()
        {
            return _eventService.GetAll();
        }

        // GET api/<EventController>/5
        [HttpGet("{id}")]
        public EventDto Get(string id)
        {
            return _eventService.Get(id);
        }

        // POST api/<EventController>
        [HttpPost]
        public void Post([FromBody] EventDto value)
        {
            _eventService.Add(value);
        }

        // PUT api/<EventController>/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] EventDto value)
        {
            _eventService.Update(id, value);

        }

        // DELETE api/<EventController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _eventService.Delete(id);
        }
    }
}
