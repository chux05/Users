using UserData.Server.Data;
using UserData.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalData.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalController : ControllerBase
    {
        private readonly ILogger<AnimalController> _logger;
        private readonly AppDbContext _context;

        public AnimalController(ILogger<AnimalController> logger, AppDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAnimals()
        {
            var result = _context.Animals.ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnimal(int id)
        {
            var result = _context.Animals.FirstOrDefault(x => x.Id == id);
            return Ok(result);
        }


        [HttpGet("limited/{number}")]
        public async Task<IActionResult> GetLimitedAnimals(int number)
        {
            var result = _context.Animals.Where(n => n.Id >= 1 && n.Id <= number);
            return Ok(result);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetAnimalsByAge([FromQuery] int AgeFrom, int AgeTo)
        {
            var result = _context.Animals.Where(q => q.Age >= AgeFrom && q.Age <= AgeTo);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAnimal([FromBody] Animal newAnimal)
        {
            var result = await _context.Animals.AddAsync(newAnimal);
            await _context.SaveChangesAsync();
            return Ok("");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {

            if (id < 1)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var Animal = _context.Animals.FirstOrDefault(x => x.Id == id);

                if (Animal == null)
                {
                    return BadRequest("Submitted data is invalid");
                }

                _context.Animals.Remove(Animal);
                await _context.SaveChangesAsync();
                return Ok("");


            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }


        [HttpPut]
        public async Task<IActionResult> UpdateAnimal([FromBody] Animal chosenAnimal)
        {
            var Animal = _context.Animals.FirstOrDefault(x => x.Id == chosenAnimal.Id);
            if (Animal == null)
            {
                return BadRequest("Submitted data is invalid");
            }
            Animal.Name = chosenAnimal.Name;
            Animal.Gender = chosenAnimal.Gender;
            Animal.Colour = chosenAnimal.Colour;

            await _context.SaveChangesAsync();
            return Ok("");
        }

       
    }
}
