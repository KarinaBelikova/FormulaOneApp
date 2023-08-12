using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using FormulaOneApp.Data;
using FormulaOneApp.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FormulaOneApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private static AppDbContext _context;

        public TeamsController(AppDbContext context)
        {
            _context = context;
        }

        /*private static List<Team> teams = new List<Team>()
        {
            new Team()
            {
                Country = "Germany",
                Id = 1,
                Name = "Mercedes AMG F1",
                TeamPrinciple = "Toto Wolf"
            },
            new Team()
            {
                Country = "Italy",
                Id = 2,
                Name = "Ferrari",
                TeamPrinciple = "Mattia Bonitto"
            },
            new Team()
            {
                Country = "Swiss",
                Id = 3,
                Name = "Alpha Romeo",
                TeamPrinciple = "Frederic Vasseur"
            },
        };*/

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var teams = await _context.Teams.ToListAsync();
            return Ok(teams);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var team = await _context.Teams.FirstOrDefaultAsync(team => team.Id == id);

            if (team == null)
                return BadRequest("Invalid Id");

            return Ok(team);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Team team)
        {
            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", team.Id, team);
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(int id, string country)
        {
            var team = await _context.Teams.FirstOrDefaultAsync(team => team.Id == id);

            if (team == null)
                return BadRequest("Invalid Id");

            team.Country = country;
            await _context.SaveChangesAsync();

            return NoContent();
;        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var team = await _context.Teams.FirstOrDefaultAsync(team => team.Id == id);

            if (team == null)
                return BadRequest("Invalid Id");

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

