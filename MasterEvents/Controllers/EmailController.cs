using Microsoft.AspNetCore.Mvc;
using Repository.Entity;
using Service.Interfaces;
using Service.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterEvents.Controllers
{
    [ApiController]
    [Route("api/email")]
    public class EmailController : ControllerBase
    {
        private readonly IMailjetService _emailService;

        public EmailController(IMailjetService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest request)
        {
            await _emailService.SendEmailAsync(request.eventId, request.Subject, request.Body);
            return Ok(new { message = "Email sent successfully!" });
        }
        //[Route("api/[controller]")]
        //[ApiController]
        //public class EmailController : ControllerBase
        //{
        //    // GET: api/<EmailController>
        //    [HttpGet]
        //    public IEnumerable<string> Get()
        //    {
        //        return new string[] { "value1", "value2" };
        //    }

        //    // GET api/<EmailController>/5
        //    [HttpGet("{id}")]
        //    public string Get(int id)
        //    {
        //        return "value";
        //    }

        //    // POST api/<EmailController>
        //    [HttpPost]
        //    public void Post([FromBody] string value)
        //    {
        //    }

        //    // PUT api/<EmailController>/5
        //    [HttpPut("{id}")]
        //    public void Put(int id, [FromBody] string value)
        //    {
        //    }

        //    // DELETE api/<EmailController>/5
        //    [HttpDelete("{id}")]
        //    public void Delete(int id)
        //    {
        //    }
    }
}
