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
using eSportMK.MVC.DTOs.Player;

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

        // ???
        // GET: api/Teams
        [HttpGet("{gameId:int}")]
        public async Task<IActionResult> GetTeamsAsync(int? gameId)
        {
            if (gameId == null)
            {
                return BadRequest();
            }
            var response = await _repo.GetFirstAsync<Game>(x => x.Id.Equals(gameId), null, nameof(Game.Teams));
            if (!response.Success)
            {
                return NotFound();
            }
            return Ok(response.Data.Teams.Select(x => new TeamDto { Name = x.Name, Game = new GameDto { Name = x.Game.Name }, Players = 
                x.Players.Select(y => new PlayerDto { FirstName = y.FirstName, LastName = y.LastName, Nickname = y.Nickname, Country = y.Country.Name }).ToList(), Country = new CountryDto { Name = x.Country.Name } }).ToList());
        }

        // GET: api/Teams/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeam([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var team = await _repo.GetOneAsync<Team>(x => x.Id.Equals(id), String.Join(",", new object[] { nameof(Team.Game), nameof(Team.Country), nameof(Team.Players) }));

            if (!team.Success)
            {
                return NotFound();
            }

            var teamDto = new TeamDto
            {
                Name = team.Data.Name,
                Game = new GameDto { Name = team.Data.Game.Name, Id = team.Data.Game.Id },
                Country = new CountryDto { Name = team.Data.Country.Name },
                Players = team.Data.Players.Select(x => new PlayerDto { FirstName = x.FirstName, LastName = x.LastName, Nickname = x.Nickname, Country = x.Country.Name }).ToList()
            };

            return Ok(teamDto);
        }

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

        // POST: api/Teams
        [HttpPost]
        public async Task<IActionResult> PostTeam([FromBody] TeamDto teamDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //MAPPING NOT DONE
            //var team = new Team
            //{
            //    Name = teamDto.Name,
            //    Country = new Country { Name = teamDto.Country.Name, },
            //    Game = new Game { Name = teamDto.Game.Name, Id = teamDto.Game.Id}
            //};

            //_context.Teams.Add(team);
            //await _context.SaveChangesAsync();

            return Ok();
        }

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