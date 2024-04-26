using AutoMail.Models.Entities;
using AutoMail.Services.Interfaces;
using AutoMail.Repository;
using Microsoft.AspNetCore.Mvc;
using AutoMail.Middleware;

namespace AutoMail.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IApplicationUserRepository _userRepository;
        private readonly IAuthenticationService _authorizationService;

        public AuthenticationController(IApplicationUserRepository userRepository, IAuthenticationService authenticationService)
        {
            _userRepository = userRepository;
            _authorizationService = authenticationService;
        }

        // GET: /user
        [RequireAuthentication]
        [HttpGet("user")]
        public IActionResult GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();
            return Ok(users);
        }

        // GET: /user/{id}
        [HttpGet("user/{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST: /user
        [HttpPost("user")]
        public IActionResult CreateUser(ApplicationUser newUser)
        {
            _userRepository.AddUser(newUser);
            return CreatedAtAction(nameof(GetUserById), new { id = newUser.ID }, newUser);
        }

        // PUT: /user/{id}
        [HttpPut("user/{id}")]
        public IActionResult UpdateUser(int id, ApplicationUser updatedUser)
        {
            if (!id.ToString().Equals(updatedUser.ID))
            {
                return BadRequest();
            }
            _userRepository.UpdateUser(updatedUser);
            return NoContent();
        }

        // DELETE: /user/{id}
        [HttpDelete("user/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            _userRepository.DeleteUser(id);
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            return Ok(await _authorizationService.LoginUserAsync(email, password));
        }
    }
}
