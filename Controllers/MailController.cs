using AutoMail.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutoMail.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MailController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IMailManagementService _mailService;

        public MailController(ILogger<WeatherForecastController> logger, IMailManagementService mailService)
        {
            _logger = logger;
            _mailService = mailService;
        }

        [HttpGet(Name = "SendMail")]
        public string SendMail() 
        {
            _mailService.SendMailAsync();
            return "发送成功";
        }
    }
}
