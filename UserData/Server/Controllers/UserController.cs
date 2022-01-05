using UserData.Server.Data;
using UserData.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserData.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly AppDbContext _context;

        public UserController(ILogger<UserController> logger, AppDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var result = _context.Users.ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var result = _context.Users.FirstOrDefault(x => x.Id == id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User newUser)
        {
            var result = await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return Ok("");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
        
            if (id < 1)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var User = _context.Users.FirstOrDefault(x => x.Id == id);

                if (User == null)
                {
                    return BadRequest("Submitted data is invalid");
                }

                _context.Users.Remove(User);
                await _context.SaveChangesAsync();
                return Ok("");


            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
           
        }


        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] User chosenUser)
        {
            var User = _context.Users.FirstOrDefault(x => x.Id == chosenUser.Id);
            if (User == null)
            {
                return BadRequest("Submitted data is invalid");
            }
            User.FirstName = chosenUser.FirstName;
            User.LastName = chosenUser.LastName;

            await _context.SaveChangesAsync();
            return Ok("");
        }
    }
}
