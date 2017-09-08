using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSportMK.MVC.DTOs;
using eSportMK.MVC.DTOs.Game;
using eSportMK.MVC.Models;
using eSportMK.MVC.Repository.BaseRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eSportMK.MVC.API
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
        public async Task<IActionResult> GetGames()
        {
            try
            {
                var response = await _repo.GetAllAsync<Game>();
                if (response.Success)
                {
                    var list = response.Data.Select(x => new GameDto {Id = x.Id, Name = x.Name}).ToList();
                    return Ok(list);
                }
                return Ok(new List<GameDto>());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGame([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
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

                return Ok(gameDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/Games/5
        [HttpPost("update/{id}")]
        public async Task<IActionResult> PutGame([FromBody] UpdateGameDto gameDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            try
            {
                var getGame = await _repo.GetByIdAsync<Game>(gameDto.Id);
                if (!getGame.Success)
                {
                    return NotFound();
                }

                var game = getGame.Data;
                game.Name = gameDto.Name;

                var update = _repo.Update<Game>(game);
                if (!update.Success)
                {
                    return Ok(false);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(gameDto.Id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                }
            }

            return Ok(true);
        }

        // POST: api/Games
        [HttpPost("create")]
        public  IActionResult PostGame([FromBody] CreateGameDto gameDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var game = new Game
                {
                    Name = gameDto.Name
                };

                var creation = _repo.Create<Game>(game);
                if (!creation.Success)
                {
                    return Ok(false);
                }

                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Games/5
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteGame([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var game = await _repo.GetByIdAsync<Game>(id);
                if (!game.Success)
                {
                    return NotFound();
                }

                var deletion = _repo.Delete<Game>(id);
                if (!deletion.Success)
                {
                    return Ok(false);
                }

                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private bool GameExists(int id)
        {
            return _repo.GetExists<Game>(e => e.Id == id).Data;
        }
    }
}