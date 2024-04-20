using AutoMail.Models.Entities;
using AutoMail.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoMail.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
        public IActionResult CreateUser(User newUser)
        {
            _userRepository.AddUser(newUser);
            return CreatedAtAction(nameof(GetUserById), new { id = newUser.ID }, newUser);
        }

        // PUT: /user/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User updatedUser)
        {
            if (id != updatedUser.ID)
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
