using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubGuestController : ControllerBase
    {
        private readonly IService<SubGuestDto> _subGuestService;
        // GET: api/<SubGuestController>
        [HttpGet]
        public IEnumerable<SubGuestDto> Get()
        {
            return _subGuestService.GetAll();
        }

        // GET api/<SubGuestController>/5
        [HttpGet("{id}")]
        public SubGuestDto Get(string id)
        {
            return _subGuestService.Get(id);
        }

        // POST api/<SubGuestController>
        [HttpPost]
        public void Post([FromBody] SubGuestDto value)
        {
            _subGuestService.Add(value);
        }

        // PUT api/<SubGuestController>/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] SubGuestDto value)
        {
            _subGuestService.Update(id, value);
        }

        // DELETE api/<SubGuestController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _subGuestService.Delete(id);
        }
    }
}
