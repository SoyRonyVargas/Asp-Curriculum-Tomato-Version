using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_24BM.Models;
using Web_24BM.Services;

namespace Web_24BM.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendEmailController : ControllerBase
    {
        private readonly IEmailSenderService _emailSenderService;

        public SendEmailController(IEmailSenderService emailSenderService)
        {
            _emailSenderService = emailSenderService;
        }

        [HttpPost]
        public IActionResult Send(MensajeViewModel model)

        {
            var result = _emailSenderService.SendEmailwithData(model);
            if(result)
            {
                return Ok(model);
            }
            else
            {
                return BadRequest();
            }
            
        }

    }
}
