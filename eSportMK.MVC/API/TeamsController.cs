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
            var response = await _repo.GetAsync<Team>(x => x.GameId.Equals(gameId), null, String.Join(",", new object[] { /*nameof(Team.Game),*/ nameof(Team.Players), nameof(Team.Country) }) );
            if (!response.Success)
            {
                return NotFound();
            }

            var teams = response.Data;
            var list = new List<TeamDto>();
            foreach (var team in teams)
            {
                var dto = new TeamDto()
                {
                    Id = team.Id,
                    Name = team.Name,
                    Country = _repo.GetFirst<Country>(x => x.Id.Equals(team.CountryId)).Data.Name,
                    Game = _repo.GetFirst<Game>(x => x.Id.Equals(team.GameId)).Data.Name,
                    Players = _repo.Get<Player>(x => x.TeamId == team.Id).Data.Select(x => x.Nickname).ToList()
                };
                list.Add(dto);
            }
            return Ok(list);
        }

        // GET: api/Teams/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeam([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var getTeam = await _repo.GetOneAsync<Team>(x => x.Id.Equals(id), String.Join(",", new object[] { nameof(Team.Game), nameof(Team.Country), nameof(Team.Players) }));
            if (!getTeam.Success)
            {
                return NotFound();
            }

            var team = getTeam.Data;
            var teamDto = new TeamDto
            {
                Id = team.Id,
                Name = team.Name,
                Country = _repo.GetFirst<Country>(x => x.Id.Equals(team.CountryId)).Data.Name,
                Game = _repo.GetFirst<Game>(x => x.Id.Equals(team.GameId)).Data.Name,
                Players = _repo.Get<Player>(x => x.TeamId.Equals(team.Id)).Data.Select(x => x.Nickname).ToList()
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
        [HttpPost("create")]
        public IActionResult PostTeam([FromBody] TeamPostDto teamDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var team = new Team()
                {
                    Name = teamDto.Name,
                    GameId = teamDto.GameId,
                    CountryId = teamDto.CountryId
                };

                var create = _repo.Create<Team>(team);
                if (!create.Success) return Ok(false);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Teams/5
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteTeam([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var team = await _repo.GetFirstAsync<Team>(x => x.Id.Equals(id));
                if (!team.Success)
                {
                    return NotFound();
                }

                var delete = _repo.Delete<Team>(id);
                if (delete.Success) return Ok(true);

                return Ok(false);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //private bool TeamExists(string id)
        //{
        //    return _context.Teams.Any(e => e.Id == id);
        //}
    }
}