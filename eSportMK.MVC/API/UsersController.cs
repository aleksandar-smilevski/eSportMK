using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eSportMK.MVC.Models;
using eSportMK.Dto;
using eSportMK.MVC.DTOs.Team;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using eSportMK.MVC.Repository.BaseRepository;

namespace eSportMK.MVC.API
{
    [Produces("application/x-www-form-urlencoded")]
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly IRepository _repo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(IRepository repo, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _repo = repo;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: api/Users
        [HttpGet("all")]
        public IEnumerable<UserDto> GetApplicationUser()
        {
            return _userManager.Users.Select(x => new UserDto() { Id = x.Id, UserName = x.UserName, Email = x.Email}).ToList();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplicationUser(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var getApplicationUser = await _repo.GetFirstAsync<ApplicationUser>(x => x.Id.Equals(id), null, nameof(ApplicationUser.Roles));

            if (!getApplicationUser.Success)
            {
                return NotFound();
            }

            var applicationUser = getApplicationUser.Data;
            var allRoles = _roleManager.Roles.ToList();

            var roles = applicationUser.Roles.Join(allRoles, ur => ur.RoleId, r => r.Id, (userRoles, role) => new string(role.Name.ToCharArray()));

            var userDto = new UserPreviewDto
            {
                Id = applicationUser.Id,
                FirstName = applicationUser.FirstName,
                LastName = applicationUser.LastName,
                Email = applicationUser.Email,
                UserName = applicationUser.UserName,
                DateOfBirth = applicationUser.DateOfBirth,
                Roles = roles.ToList()
            };
            return Ok(userDto);
        }

        // PUT: api/Users/5
        [HttpPut("update/{id}")]
        public async Task<IActionResult> PutApplicationUser([FromRoute] string id, [FromBody] UserPreviewDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                if (id != userDto.Id)
                {
                    return BadRequest();
                }

                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                user.FirstName = userDto.FirstName;
                user.LastName = userDto.LastName;
                user.DateOfBirth = userDto.DateOfBirth;

                var update = await _userManager.UpdateAsync(user);
                if (update.Succeeded)
                    return Ok(true);

                return Ok(false);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("addToRole")]
        public async Task<IActionResult> AddToRole(string userId, string roleName)
        {
            try
            {
                if (userId == null || roleName == null)
                {
                    return BadRequest();
                }
                var user = await _userManager.FindByIdAsync(userId);
                var role = await _roleManager.FindByNameAsync(roleName.ToUpper());

                if (user == null || role == null)
                {
                    return NotFound();
                }

                var add = await _userManager.AddToRoleAsync(user, role.Name);
                return Ok(add.Succeeded);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: api/Users
        [HttpPost("create")]
        public async Task<IActionResult> PostApplicationUser([FromBody] CreateUserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = new ApplicationUser
                {
                    UserName = userDto.UserName,
                    LastName = userDto.LastName,
                    Email = userDto.Email,
                    DateOfBirth = userDto.DateOfBirth,
                    FirstName = userDto.FirstName
                };

                var create = await _userManager.CreateAsync(user, userDto.Password);
                if (create.Succeeded)
                    return Ok(true);

                return Ok(false);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Users/5
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteApplicationUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var applicationUser = await _userManager.FindByIdAsync(id);
                if (applicationUser == null)
                {
                    return NotFound();
                }

                var delete = await _userManager.DeleteAsync(applicationUser);
                if (delete.Succeeded)
                    return Ok(true);

                return Ok(false);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}