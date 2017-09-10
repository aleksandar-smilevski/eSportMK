using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSportMK.MVC.DTOs.Location;
using eSportMK.MVC.DTOs.Tournament;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eSportMK.MVC.Models;
using eSportMK.MVC.Repository.BaseRepository;

namespace eSportMK.MVC.API
{
    [Produces("application/json")]
    [Route("api/Tournaments")]
    public class TournamentsController : Controller
    {
        private readonly IRepository _repo;

        public TournamentsController(IRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Tournaments
        [HttpGet("all/{gameId:int}")]
        public async Task<IActionResult> GetTournaments(int? gameId)
        {
            if (gameId == null)
            {
                return BadRequest();
            }
            var response = await _repo.GetAsync<Tournament>(x => x.Game.Id.Equals(gameId), null, String.Join(",", new List<string>{ nameof(Tournament.Location) }));
            if (!response.Success)
            {
                return NotFound();
            }

            var tournaments = response.Data;
            var list = new List<TournamentDto>();
            foreach (var tourney in tournaments)
            {
                var dto = new TournamentDto()
                {
                    Id = tourney.Id,
                    Name = tourney.Name,
                    Date = tourney.Date,
                    Description = tourney.Description,
                    Game = _repo.GetById<Game>(tourney.Game.Id).Data.Name,
                    //Location = new LocationDto
                    //{
                    //    Country = _repo.GetById<Location>().Data.Country.Name,
                    //    City = _repo.GetById<Location>(tourney.LocationId).Data.City
                    //}
                };
                list.Add(dto);
            }
            return Ok(list);
        }

        //// GET: api/Tournaments/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetTournament([FromRoute] string id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var tournament = await _context.Tournaments.SingleOrDefaultAsync(m => m.Id == id);

        //    if (tournament == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(tournament);
        //}

        //// PUT: api/Tournaments/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTournament([FromRoute] string id, [FromBody] Tournament tournament)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != tournament.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(tournament).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TournamentExists(id))
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

        //// POST: api/Tournaments
        //[HttpPost]
        //public async Task<IActionResult> PostTournament([FromBody] Tournament tournament)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Tournaments.Add(tournament);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetTournament", new { id = tournament.Id }, tournament);
        //}

        //// DELETE: api/Tournaments/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTournament([FromRoute] string id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var tournament = await _context.Tournaments.SingleOrDefaultAsync(m => m.Id == id);
        //    if (tournament == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Tournaments.Remove(tournament);
        //    await _context.SaveChangesAsync();

        //    return Ok(tournament);
        //}

        //private bool TournamentExists(string id)
        //{
        //    return _context.Tournaments.Any(e => e.Id == id);
        //}
    }
}