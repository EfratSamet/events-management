using Microsoft.AspNetCore.Mvc;
using Service.Dtos;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestInEventController : ControllerBase
    {
        private readonly IService<GuestInEventDto> _guestInEventService;
        public GuestInEventController(IService<GuestInEventDto> guestInEventService)
        {
            _guestInEventService = guestInEventService;
        }

        // GET: api/<GuestInEventController>
        [HttpGet]
        public IEnumerable<GuestInEventDto> Get()
        {
            return _guestInEventService.GetAll();
        }

        // GET api/<GuestInEventController>/5
        [HttpGet("{id}")]
        public GuestInEventDto Get(string id)
        {
            return _guestInEventService.Get(id);
        }

        // POST api/<GuestInEventController>
        [HttpPost]
        public void Post([FromBody] GuestInEventDto value)
        {
            _guestInEventService.Add(value);
        }

        // PUT api/<GuestInEventController>/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] GuestInEventDto value)
        {
            _guestInEventService.Update(id, value);
        }

        // DELETE api/<GuestInEventController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _guestInEventService.Delete(id);
        }
    }
}
