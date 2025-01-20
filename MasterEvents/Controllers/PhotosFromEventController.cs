using Microsoft.AspNetCore.Mvc;
using Service.Dtos;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosFromEventController : ControllerBase
    {
        private readonly IService<PhotosFromEventDto> _photosFromEventService;
        // GET: api/<PhotosFromEventController>
        [HttpGet]
        public IEnumerable<PhotosFromEventDto> Get()
        {
            return _photosFromEventService.GetAll();
        }

        // GET api/<PhotosFromEventController>/5
        [HttpGet("{id}")]
        public PhotosFromEventDto Get(string id)
        {
            return _photosFromEventService.Get(id);
        }

        // POST api/<PhotosFromEventController>
        [HttpPost]
        public void Post([FromBody] PhotosFromEventDto value)
        {
            _photosFromEventService.Add(value);
        }

        // PUT api/<PhotosFromEventController>/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] PhotosFromEventDto value)
        {
            _photosFromEventService.Update(id, value);
        }

        // DELETE api/<PhotosFromEventController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _photosFromEventService.Delete(id);
        }
    }
}
