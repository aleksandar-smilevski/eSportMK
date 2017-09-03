using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eSportMK.MVC.Models;
using eSportMK.Dto;
using eSportMK.Repository.BaseRepository;
using eSportMK.MVC.DTOs;
using eSportMK.MVC.DTOs.Team;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace eSportMK.MVC.API
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private readonly IRepository _repo;

        public UsersController(IRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<UserDto> GetApplicationUser()
        {
            return _repo.GetAll<ApplicationUser>().Data.Select(x => new UserDto() { Id = x.Id, UserName = x.UserName, Email = x.Email}).ToList();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplicationUser(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicationUser = await _repo.GetFirstAsync<ApplicationUser>(x => x.Id.Equals(id), null, nameof(ApplicationUser.Roles));

            if (!applicationUser.Success)
            {
                return NotFound();
            }

            var user = applicationUser.Data;
            var allRoles = _repo.GetAll<IdentityRole>().Data;

            var roles = user.Roles.Join(allRoles, ur => ur.RoleId, r => r.Id, (userRoles, role) => new string(role.Name.ToCharArray()));

            var userDto = new UserPreviewDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                DateOfBirth = user.DateOfBirth,
                Roles = roles.ToList()
            };
            return Ok(userDto);
        }

        //// PUT: api/Users/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutApplicationUser([FromRoute] string id, [FromBody] ApplicationUser applicationUser)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != applicationUser.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(applicationUser).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ApplicationUserExists(id))
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

        //// POST: api/Users
        //[HttpPost]
        //public async Task<IActionResult> PostApplicationUser([FromBody] ApplicationUser applicationUser)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.ApplicationUser.Add(applicationUser);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetApplicationUser", new { id = applicationUser.Id }, applicationUser);
        //}

        //// DELETE: api/Users/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteApplicationUser([FromRoute] string id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
        //    if (applicationUser == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.ApplicationUser.Remove(applicationUser);
        //    await _context.SaveChangesAsync();

        //    return Ok(applicationUser);
        //}

        //private bool ApplicationUserExists(string id)
        //{
        //    return _context.ApplicationUser.Any(e => e.Id == id);
        //}
    }
}