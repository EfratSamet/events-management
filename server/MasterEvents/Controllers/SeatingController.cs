using Microsoft.AspNetCore.Mvc;
using Service.Dtos;
using Service.Interfaces;

namespace MasterEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatingController : ControllerBase
    {
        private readonly ISeatingService _seatingService;

        public SeatingController(ISeatingService seatingService)
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
        public SeatingDto Get(int id)
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
        public void Put(int id, [FromBody] SeatingDto value)
        {
            _seatingService.Update(id, value);
        }

        // DELETE api/<SeatingController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _seatingService.Delete(id);
        }

        // GET api/<SeatingController>/subGuestsByGuestId/{guestId}
        // מחזיר רשימת מזהי אורחים לפי מזהה אורח
        [HttpGet("subGuestsByGuestId/{guestId}")]
        public List<int> GetSubGuestsIdsByGuestId(int guestId)
        {
            return _seatingService.GetSubGuestsIdsByGuestId(guestId);
        }

        // GET api/<SeatingController>/subGuestsByTable/{tableNumber}
        // מחזיר רשימת מזהי אורחים שיושבים בשולחן מסוים
        [HttpGet("subGuestsByTable/{tableNumber}")]
        public List<int> GetSubGuestsIdsByTable(int tableNumber)
        {
            return _seatingService.GetSubGuestsIdsByTable(tableNumber);
        }

        // GET api/<SeatingController>/tableByGuestId/{guestId}
        // מחזיר את מספר השולחן לפי מזהה אורח
        [HttpGet("tableByGuestId/{guestId}")]
        public int? GetTableByGuestId(int guestId)
        {
            return _seatingService.GetTableByGuestId(guestId);
        }

        // POST api/<SeatingController>/assignSeats
        // פונקציה חדשה שתשבץ את המיקומים
        [HttpPost("assignSeats")]
        public void AssignSeats([FromBody] List<SeatingDto> seatings)
        {
            _seatingService.AssignSeats(seatings);
        }
    }
}
