using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eSportMK.MVC.DTOs.Team;
using eSportMK.MVC.Models;
using eSportMK.MVC.DTOs.Country;
using eSportMK.MVC.DTOs;
using eSportMK.MVC.DTOs.Player;
using eSportMK.MVC.Repository.BaseRepository;

namespace eSportMK.MVC.API
{
    [Produces("application/json")]
    [Route("api/teams")]
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
            var response = await _repo.GetAsync<Team>(x => x.GameId.Equals(gameId), null, String.Join(",", new object[] { nameof(Team.Game), nameof(Team.Players), nameof(Team.Country) }) );
            if (!response.Success)
            {
                return NotFound();
            }

            var teams = response.Data;
            return Ok(teams.Select(x => new TeamDto() { Id = x.Id, Name = x.Name, Country = new CountryDto() {Id = x.CountryId, Name = x.Country.Name}, Game = new GameDto(){Id = x.GameId, Name = x.Name }}).ToList());
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
            var players = await _repo.GetAsync<Player>(x => x.TeamId.Equals(id));
            if (!team.Success)
            {
                return NotFound();
            }

            var teamDto = new TeamDto
            {
                Name = team.Data.Name,
                Game = new GameDto { Name = team.Data.Game.Name, Id = team.Data.Game.Id },
                Country = new CountryDto { Name = team.Data.Country.Name },
                Players = players.Data.Any() ? players.Data.Select(x => new PlayerDto() { Nickname = x.Nickname }).ToList(): new List<PlayerDto>()
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
        public IActionResult PostTeam([FromBody] TeamDto teamDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var team = new Team
            //{
            //    Name = teamDto.Name,
            //    C = teamDto.Country.Id
            //    Game = new Game { Name = teamDto.Game.Name, Id = teamDto.Game.Id }
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