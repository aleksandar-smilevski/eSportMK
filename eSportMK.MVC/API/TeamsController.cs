using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eSportMK.Repository.BaseRepository;
using eSportMK.MVC.DTOs.Team;
using eSportMK.MVC.Models;
using eSportMK.MVC.DTOs.Country;
using eSportMK.MVC.DTOs;

namespace eSportMK.MVC.API
{
    [Produces("application/json")]
    [Route("api/Teams")]
    public class TeamsController : Controller
    {
        private readonly IRepository _repo;

        public TeamsController(IRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Teams
        [HttpGet]
        public IEnumerable<TeamDto> GetTeams()
        {
            var response = _repo.GetAll<Team>(null, "Players,Game");
            if (response.Success)
            {
                var list = response.Data.Select(x => new TeamDto { Name = x.Name, Game = new GameDto { Name = x.Game.Name }, Country = new CountryDto { Name = x.Country.Name}, Players = x.Players.Select(y => new DTOs.Player.PlayerDto { FirstName = y.FirstName, LastName = y.LastName, Country = y.Country }).ToList() }).ToList();
                return list;
            }
            return new List<TeamDto>();
        }

        //// GET: api/Teams/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetTeam([FromRoute] string id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var team = await _context.Teams.SingleOrDefaultAsync(m => m.Id == id);

        //    if (team == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(team);
        //}

        //// PUT: api/Teams/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTeam([FromRoute] string id, [FromBody] Team team)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != team.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(team).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TeamExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Teams
        //[HttpPost]
        //public async Task<IActionResult> PostTeam([FromBody] Team team)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Teams.Add(team);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetTeam", new { id = team.Id }, team);
        //}

        //// DELETE: api/Teams/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTeam([FromRoute] string id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var team = await _context.Teams.SingleOrDefaultAsync(m => m.Id == id);
        //    if (team == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Teams.Remove(team);
        //    await _context.SaveChangesAsync();

        //    return Ok(team);
        //}

        //private bool TeamExists(string id)
        //{
        //    return _context.Teams.Any(e => e.Id == id);
        //}
    }
}