using AutoMail.Models.Entities;
using AutoMail.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AutoMail.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailConfigurationController : ControllerBase
    {
        private readonly IEmailConfigurationRepository _emailConfigurationRepository;

        public EmailConfigurationController(IEmailConfigurationRepository emailConfigurationRepository)
        {
            _emailConfigurationRepository = emailConfigurationRepository;
        }

        // GET: /EmailConfiguration
        [HttpGet]
        public IActionResult GetAllEmailConfigurations()
        {
            var emailConfigurations = _emailConfigurationRepository.GetAllEmailConfigurations();
            return Ok(emailConfigurations);
        }

        // GET: /EmailConfiguration/{id}
        [HttpGet("{id}")]
        public IActionResult GetEmailConfigurationById(int id)
        {
            var emailConfiguration = _emailConfigurationRepository.GetByEmailConfigurationId(id);
            if (emailConfiguration == null)
            {
                return NotFound();
            }
            return Ok(emailConfiguration);
        }

        // POST: /EmailConfiguration
        [HttpPost]
        public IActionResult AddEmailConfiguration(EmailConfiguration emailConfiguration)
        {
            _emailConfigurationRepository.AddEmailConfiguration(emailConfiguration);
            return CreatedAtAction(nameof(GetEmailConfigurationById), new { id = emailConfiguration.ID }, emailConfiguration);
        }

        // PUT: /EmailConfiguration/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateEmailConfiguration(int id, EmailConfiguration emailConfiguration)
        {
            if (id != emailConfiguration.ID)
            {
                return BadRequest();
            }
            _emailConfigurationRepository.UpdateEmailConfiguration(emailConfiguration);
            return NoContent();
        }

        // DELETE: /EmailConfiguration/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteEmailConfiguration(int id)
        {
            _emailConfigurationRepository.DeleteEmailConfiguration(id);
            return NoContent();
        }
    }
}
