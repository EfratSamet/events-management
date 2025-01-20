using Microsoft.AspNetCore.Mvc;
using Service.Dtos;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly IService<GuestDto> _guestService;
        // GET: api/<GuestController>
        [HttpGet]
        public IEnumerable<GuestDto> Get()
        {
            return _guestService.GetAll();
        }

        // GET api/<GuestController>/5
        [HttpGet("{id}")]
        public GuestDto Get(string id)
        {
            return _guestService.Get(id);
        }

        // POST api/<GuestController>
        [HttpPost]
        public void Post([FromBody] GuestDto value)
        {
            _guestService.Add(value);
        }

        // PUT api/<GuestController>/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] GuestDto value)
        {
            _guestService.Update(id, value);
        }

        // DELETE api/<GuestController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _guestService.Delete(id);   
        }
    }
}
