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
        // GET: api/<OrganizerController>
        [HttpGet]
        public IEnumerable<OrganizerDto> Get()
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
        public void Post([FromBody] OrganizerDto value)
        {
            _organizerService.Add(value);
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
    }
}
