using DreamProperties.API.Models;
using DreamProperties.API.Services;
using DreamProperties.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DreamProperties.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IMailService _mailService;

        public EmailController(ILogger<EmailController> logger,
                               IMailService mailService)
        {
            _logger = logger;
            _mailService = mailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] MailDTO mailDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var request = new MailRequest
                {
                    ToEmail = mailDTO.ToEmail,
                    Subject = $"Enquiry about property {mailDTO.PropertyTitle}",
                    Body = $"Hi,<br/> could you please send more information about property '{mailDTO.PropertyTitle}' " +
                    $"to the email {mailDTO.FromEmail}?<br/><br/>Thanks.<br/><br/> Your DreamProperties app"
                };

                await _mailService.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(SendEmail)}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }

            return null;
        }

    }
}
