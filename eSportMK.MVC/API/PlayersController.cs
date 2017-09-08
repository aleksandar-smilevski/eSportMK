using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eSportMK.MVC.Models;
using eSportMK.Repository.BaseRepository;
using eSportMK.MVC.DTOs.Player;
using eSportMK.MVC.DTOs.Team;

namespace eSportMK.MVC.API
{
    [Produces("application/json")]
    [Route("api/players")]
    public class PlayersController : Controller
    {
        private readonly IRepository _repo;

        public PlayersController(IRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Players
        /// <summary>
        /// 1 - Dota2, 2 - CSGO, 3 - LOL, 4 - Overwatch
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        [HttpGet("{gameId:int}")]
        public async Task<IActionResult> GetPlayersAsync(int? gameId)
        {
            if (gameId == null)
            {
                return BadRequest();
            }
            var response = await _repo.GetAsync<Player>(x => x.Game.Id.Equals(gameId), null, String.Join(",", new object[] { nameof(Player.Game), nameof(Player.Country), nameof(Player.Team), nameof(Player.Statistics) }));
            if (!response.Success)
            {
                return NotFound();
            }
            return Ok( response.Data.Select(x => new PlayerDto { FirstName = x.FirstName, Country = x.Country.Name, LastName = x.LastName, Nickname = x.Nickname, Team = x.Team.Name }).ToList());
        }

        // GET: api/Players/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayer([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var getPlayer = await _repo.GetFirstAsync<Player>(x => x.Id.Equals(id), null, String.Join(",", new object[] { nameof(Player.Country), nameof(Player.Game), nameof(Player.Team) }));

            if (!getPlayer.Success)
            {
                return NotFound();
            }
            var player = getPlayer.Data;

            var playerDto = new PlayerDto
            {
                FirstName = player.FirstName,
                LastName = player.LastName,
                Nickname = player.Nickname,
                Country = player.Country.Name,
                DateOfBirth = player.DateOfBirth,
                Team = player.Team.Name,
                Game = player.Game.Name
            };

            return Ok(playerDto);
        }

        //// PUT: api/Players/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutPlayer([FromRoute] string id, [FromBody] Player player)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != player.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(player).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PlayerExists(id))
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

        //// POST: api/Players
        //[HttpPost]
        //public async Task<IActionResult> PostPlayer([FromBody] Player player)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Players.Add(player);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetPlayer", new { id = player.Id }, player);
        //}

        // DELETE: api/Players/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var getPlayer = await _repo.GetFirstAsync<Player>(x => x.Id.Equals(id), null, String.Join(",", new object[] { nameof(Player.Country), nameof(Player.Game), nameof(Player.Team) }));
                if (!getPlayer.Success)
                {
                    return NotFound();
                }

                var delete = _repo.Delete<Player>(id);
                if(delete.Success)
                    return Ok(true);

                return Ok(false);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //private bool PlayerExists(string id)
        //{
        //    return _context.Players.Any(e => e.Id == id);
        //}
    }
}