
﻿using Microsoft.AspNetCore.Mvc;
using Service.Dtos;
using Service.Interfaces;
using Service.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly IGuestService _guestService;
        public GuestController(IGuestService guestService)
        {
            _guestService = guestService;
        }

        // GET: api/<GuestController>
        [HttpGet]
        public IEnumerable<GuestDto> Get()
        {
            return _guestService.GetAll();
        }

        // GET api/<GuestController>/5
        [HttpGet("{id}")]
        public GuestDto Get(int id)
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
        public void Put(int id, [FromBody] GuestDto value)
        {
            _guestService.Update(id, value);
        }

        // DELETE api/<GuestController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _guestService.Delete(id);
        }
        [HttpGet("organizer/{organizerId}")]
        public IActionResult GetGuestsByOrganizer(int organizerId)
        {
            var guests = _guestService.GetGuestsByOrganizerId(organizerId);
            if (guests == null || !guests.Any())
            {
                return NotFound("No guests found for this organizer.");
            }
            return Ok(guests);
        }
        [HttpGet("event/{eventId}")]
        public IActionResult GetGuestsByEvent(int eventId)
        {
            var guests = _guestService.GetGuestsByEventId(eventId);
            if (guests == null || !guests.Any())
            {
                return NotFound("No guests found for this event.");
            }
            return Ok(guests);
        }
        [HttpGet("group/{groupId}")]
        public IActionResult GetGuestsByGroup(int groupId)
        {
            var guests = _guestService.GetGuestsByGroup(groupId);
            if (guests == null || !guests.Any())
            {
                return NotFound("No guests found for this event.");
            }
            return Ok(guests);
        }  
    }
}
