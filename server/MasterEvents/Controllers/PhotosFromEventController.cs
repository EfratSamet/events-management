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
        public static string _directory = Environment.CurrentDirectory + "/images/";
        private readonly IService<PhotosFromEventDto> _photosFromEventService;
        public PhotosFromEventController(IService<PhotosFromEventDto> photosFromEventService)
        {
            _photosFromEventService = photosFromEventService;
        }

        // GET: api/<PhotosFromEventController>
        [HttpGet]
        public List<PhotosFromEventDto> Get()
        {
            return _photosFromEventService.GetAll();
        }

        // GET api/<PhotosFromEventController>/5
        [HttpGet("{id}")]
        public PhotosFromEventDto Get(int id)
        {
            return _photosFromEventService.Get(id);
        }

        // POST api/<PhotosFromEventController>
        [HttpPost]
        public PhotosFromEventDto Post([FromBody] PhotosFromEventDto value)
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Images/", value.File.FileName); //l:/...
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                value.File.CopyTo(fs);
                fs.Close();
            }
            var res = _photosFromEventService.Add(value);
            return res;
        }

        // PUT api/<PhotosFromEventController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] PhotosFromEventDto value)
        {
            _photosFromEventService.Update(id, value);
        }

        // DELETE api/<PhotosFromEventController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _photosFromEventService.Delete(id);
        }
    }
}
