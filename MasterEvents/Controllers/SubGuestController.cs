using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Dtos;

namespace MasterEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubGuestController : ControllerBase
    {
        private readonly ISubGuestService _subGuestService;

        public SubGuestController(ISubGuestService subGuestService)
        {
            _subGuestService = subGuestService;
        }

        // מחזיר את כל תתי-האורחים
        [HttpGet]
        public ActionResult<List<SubGuestDto>> Get()
        {
            return Ok(_subGuestService.GetAll());
        }

        // מחזיר תת-אורח לפי ה-ID שלו
        [HttpGet("subguest/{id}")]
        public ActionResult<SubGuestDto> Get(int id)
        {
            var subGuest = _subGuestService.Get(id);
            return subGuest != null ? Ok(subGuest) : NotFound("תת-אורח לא נמצא.");
        }

        // מחזיר את כל תתי-האורחים של אורח מסוים לפי ה-guestId שלו
        [HttpGet("guest/{guestId}")]
        public ActionResult<List<SubGuestDto>> GetSubGuestByGuestId(int guestId)
        {
            var subGuests = _subGuestService.GetSubGuestsByGuestId(guestId);
            return subGuests.Any() ? Ok(subGuests) : NotFound("לא נמצאו תתי-אורחים.");
        }

        // הוספת תת-אורח חדש
        [HttpPost]
        public IActionResult Post([FromBody] SubGuestDto value)
        {
            _subGuestService.Add(value);
            return CreatedAtAction(nameof(Get), new { id = value.id }, value);
        }

        // עדכון תת-אורח לפי ID
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] SubGuestDto value)
        {
            _subGuestService.Update(id, value);
            return NoContent();
        }

        // מחיקת תת-אורח לפי ID
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _subGuestService.Delete(id);
            return NoContent();
        }
        [HttpGet("event/{eventId}/guest/{guestId}")]
        public async Task<ActionResult<List<SubGuestDto>>> GetSubGuestByGuestUdAndEventId(int eventId, int guestId)
        {
            return _subGuestService.GetSubGuestByGuestUdAndEventId(eventId, guestId);
        }
    }
}
