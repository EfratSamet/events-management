using Microsoft.AspNetCore.Mvc;
using Service.Dtos;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatingController : ControllerBase
    {
        private readonly IService<SeatingDto> _seatingService;
        public SeatingController(IService<SeatingDto> seatingService)
        {
            _seatingService = seatingService;
        }
        // GET: api/<SeatingController>
        [HttpGet]
        public List<SeatingDto> Get()
        {
            return _seatingService.GetAll();
        }

        // GET api/<SeatingController>/5
        [HttpGet("{id}")]
        public SeatingDto Get(string id)
        {
            return _seatingService.Get(id);
        }

        // POST api/<SeatingController>
        [HttpPost]
        public void Post([FromBody] SeatingDto value)
        {
            _seatingService.Add(value);
        }

        // PUT api/<SeatingController>/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] SeatingDto value)
        {
            _seatingService.Update(id, value);
        }

        // DELETE api/<SeatingController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _seatingService.Delete(id); 
        }
    }
}
