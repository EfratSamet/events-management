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

        //[HttpPost("send")]
        //public async Task<IActionResult> SendEmail([FromBody] EmailRequest request)
        //{
        //    await _emailService.SendEmailAsync(request.eventId, request.Subject, request.Body);
        //    return Ok(new { message = "Email sent successfully!" });
        //}

        [HttpPost("sendSingle")]
        public async Task<IActionResult> SendSingleEmail([FromBody] EmailRequest request)
        {
            // קריאה לפונקציה SendSingleEmailAsync ששולחת מייל אחד
            await _emailService.SendSingleEmailAsync(request.ToEmail, request.Subject, request.Body);
            return Ok(new { message = "Single email sent successfully!" });
        }
    }


}
