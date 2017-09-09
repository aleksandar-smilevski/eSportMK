using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eSportMK.MVC.Models;
using eSportMK.MVC.DTOs.Player;
using eSportMK.MVC.DTOs.Team;
using eSportMK.MVC.Repository.BaseRepository;

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
            return Ok(response.Data.Select(x => new PlayerDto {Id = x.Id, FirstName = x.FirstName, Country = x.Country.Name, LastName = x.LastName, Nickname = x.Nickname, Team = x.Team.Name, Game = x.Game.Name }).ToList());
        }

        // GET: api/Players/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayer([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var getPlayer = await _repo.GetFirstAsync<Player>(x => x.Id.Equals(id), null,
                    String.Join(",", new object[] {nameof(Player.Country), nameof(Player.Game), nameof(Player.Team)}));

                if (!getPlayer.Success)
                {
                    return NotFound();
                }
                var player = getPlayer.Data;

                var playerDto = new PlayerDto
                {
                    Id = player.Id,
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
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/Players/5
        [HttpPost("update/{id}")]
        public IActionResult PutPlayer([FromRoute] string id, [FromBody] PlayerDetailsDto playerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != playerDto.Id)
            {
                return BadRequest();
            }

            try
            {
                var getPlayer = _repo.GetFirst<Player>(x => x.Id.Equals(id), null,
                    String.Join(",",
                        new object[]
                        {
                            nameof(Player.Game), nameof(Player.Country), nameof(Player.Team), nameof(Player.Statistics)
                        }));

                if (!getPlayer.Success)
                {
                    return NotFound();
                }

                var player = getPlayer.Data;

                player.FirstName = playerDto.FirstName ?? player.FirstName;
                player.LastName = playerDto.LastName ?? player.LastName;
                player.Nickname = playerDto.Nickname ?? player.Nickname;
                player.TeamId = playerDto.TeamId ?? player.TeamId;

                var update = _repo.Update<Player>(player);
                return Ok(update.Data);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
                {
                    return NotFound();
                }
                return BadRequest();
            }
        }

        // POST: api/Players
        [HttpPost("create")]
        public IActionResult PostPlayer([FromBody] PlayerDetailsDto playerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var player = new Player
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = playerDto.FirstName,
                    LastName = playerDto.LastName,
                    Nickname = playerDto.Nickname,
                    DateOfBirth = playerDto.DateOfBirth,
                    CountryId = playerDto.CountryId,
                    GameId = playerDto.GameId,
                    TeamId = playerDto.TeamId
                };

                var create = _repo.Create<Player>(player);
                return Ok(create.Success);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // DELETE: api/Players/5
        [HttpPost("delete/{id}")]
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

        private bool PlayerExists(string id)
        {
            return _repo.GetExists<Player>(e => e.Id == id).Data;
        }
    }
}