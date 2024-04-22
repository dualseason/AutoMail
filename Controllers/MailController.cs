using AutoMail.Models.ViewModels;
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

        [HttpPost("send")]
        public async Task<IActionResult> SendMail([FromBody] SendMailRequest request)
        {
            // 根据请求参数调用 SendMailAsync 方法
            try
            {
                await _mailService.SendMailAsync(request.EmailConfigID, request.ReceiveEmail, request.Subject, request.Body);
                return Ok("邮件发送成功");
            }
            catch (Exception ex)
            {
                _logger.LogError($"发送邮件时发生错误：{ex.Message}");
                return StatusCode(500, "发送邮件时发生错误");
            }
        }
    }
}
