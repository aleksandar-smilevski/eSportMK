using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eSportMK.Repository.BaseRepository;
using eSportMK.MVC.DTOs;
using eSportMK.MVC.DTOs.Game;
using eSportMK.MVC.Models;

namespace eSportMK.MVC
{
    [Produces("application/json")]
    [Route("api/games")]
    public class GamesController : Controller
    {
        private readonly IRepository _repo;

        public GamesController(IRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Games
        [HttpGet]
        public IEnumerable<GameDto> GetGames()
        {
            var response = _repo.GetAll<Game>();
            if (response.Success)
            {
                var list = response.Data.Select(x => new GameDto { Id = x.Id, Name = x.Name }).ToList();
                return list;
            }
            return new List<GameDto>();
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGame([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var game = await _repo.GetByIdAsync<Game>(id);

            if (!game.Success)
            {
                return NotFound();
            }

            var gameDto = new GameDto
            {
                Id = game.Data.Id,
                Name = game.Data.Name
            };

            return Ok(game);
        }

        // PUT: api/Games/5
        [HttpPost("update/{id}")]
        public async Task<IActionResult> PutGame([FromBody] UpdateGameDto gameDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var getGame = await _repo.GetByIdAsync<Game>(gameDto.Id);
            if (!getGame.Success)
            {
                return NotFound();
            }

            var game = getGame.Data;
            game.Name = gameDto.Name;
            try
            {
                var update = _repo.Update<Game>(game);
                if (!update.Success)
                {
                    return BadRequest(update.ErrorMessage);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(game.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Games
        [HttpPost("create")]
        public  IActionResult PostGame([FromBody] CreateGameDto gameDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var game = new Game
            {
                Name = gameDto.Name
            };

            var creation = _repo.Create<Game>(game);
            if (!creation.Success)
            {
                return BadRequest(creation.ErrorMessage);
            }

            return CreatedAtAction("GetGame", new { id = game.Id }, game);
        }

        // DELETE: api/Games/5
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteGame([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var game = await _repo.GetByIdAsync<Game>(id);
            if (!game.Success)
            {
                return NotFound();
            }

            var deletion = _repo.Delete<Game>(id);
            if (!deletion.Success)
            {
                return BadRequest(deletion.ErrorMessage);
            }

            return Ok(game);
        }

        private bool GameExists(int id)
        {
            return _repo.GetExists<Game>(e => e.Id == id).Data;
        }
    }
}