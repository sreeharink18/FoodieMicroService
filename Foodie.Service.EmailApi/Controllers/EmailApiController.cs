using Foodie.Service.EmailApi.ExternalServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Foodie.Service.EmailApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailApiController : ControllerBase
    {
        private EmailService _emailService;
        public EmailApiController(EmailService emailService) {
            _emailService = emailService;
        }
        [HttpPost("SampleTesting")]
        public async Task<IActionResult> SampleTesting([FromBody]string email)
        {
            _emailService.RegisterUserEmailAndLog(email).GetAwaiter().GetResult();
            return Ok();
        }
    }
}
