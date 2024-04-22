using AutoMail.Models.Entities;
using AutoMail.Models.ViewModels;
using AutoMail.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoMail.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

        public UserController(IUserRepository userRepository, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IPasswordHasher<ApplicationUser> passwordHasher)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
        }

        // GET: /user
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();
            return Ok(users);
        }

        // GET: /user/{id}
        [HttpGet("{id}")]
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
        [HttpPost]
        public IActionResult CreateUser(ApplicationUser newUser)
        {
            _userRepository.AddUser(newUser);
            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
        }

        // PUT: /user/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, ApplicationUser updatedUser)
        {
            if (!id.ToString().Equals(updatedUser.Id))
            {
                return BadRequest();
            }
            _userRepository.UpdateUser(updatedUser);
            return NoContent();
        }

        // DELETE: /user/{id}
        [HttpDelete("{id}")]
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
    }
}
