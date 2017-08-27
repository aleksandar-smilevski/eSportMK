using System;
using eSportMK.Repository.Interfaces;
using System.Collections.Generic;
using eSportMK.Data;
using eSportMK.Database;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using eSportMK.Repository.ResponseObject;
using System.Threading.Tasks;
using CryptoHelper;
using eSportMK.Dto;
using eSportMK.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace eSportMK.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Response<bool> AssignUserToRole(string userId, RoleDto role)
        {
            var response = new Response<bool> { Data = false };
            var user = _db.Users.Include(x => x.Role).FirstOrDefault(x => x.Id.ToString().Equals(userId));
            var roleToAdd = _db.Roles.FirstOrDefault(x => x.Id.Equals(role.Id));
            if (user == null)
            {
                response.ErrorMessage = "No such user found!";
                response.Success = false;
                return response;
            }

            if (roleToAdd == null)
            {
                response.ErrorMessage = "No such role found!";
                response.Success = false;
                return response;
            }

            if (user.Role != null)
            {
                response.ErrorMessage = "User is already in role";
                response.Success = false;
                return response;
            }

            try
            {
                user.Role = role;
                _db.Entry(user).State = EntityState.Modified;
                _db.SaveChanges();
                response.Data = true;
                response.Success = true;
                return response;
            }
            catch (DbUpdateException ex)
            {
                response.ErrorMessage = "Adding user to role failed. Try again! " + ex;
                response.Success = false;
                return response;
            }

        }

        public Response<bool> UserNameExists(string userName)
        {
            var response = new Response<bool> { Data = true };
            var user = GetUserByUserName(userName);
            if (user.Data != null)
            {
                response.ErrorMessage = "User already exists";
                response.Success = false;
                return response;
            }
            
            response.Success = true;
            response.Data = false;
            return response;
        }

        public Response<User> Create(CreateUserDto user)
        {
            var response = new Response<User> { Data = null };
            var userExists = GetUserByUserName(user.UserName);
            if (userExists.Data != null)
            {
                response.ErrorMessage = "User already exists";
                response.Success = false;
                return response;
            }
            try
            {
                var newUser = new User()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateOfBirth = user.DateOfBirth,
                    IsActive = true,
                    PasswordHash = Crypto.HashPassword(user.Password),
                    Role = null
                };
                _db.Users.Add(newUser);
                _db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                response.ErrorMessage = "Failed to create user";
                response.Success = false;
                return response;
            }
            var addedUser = GetUserByUserName(user.UserName);
            if (addedUser.Data == null)
            {
                response.ErrorMessage = "Failed to create user";
                response.Success = false;
                return response;
            }
            response.Data = addedUser.Data;
            response.Success = true;
            return response;
        }

        public async Task<Response<bool>> Delete(string userId)
        {
            var response = new Response<bool> { Data = false };
            try
            {
                var userExists = await _userManager.FindByIdAsync(userId);
                if (userExists == null)
                {
                    response.ErrorMessage = "User does not exist";
                    response.Success = false;
                    return response;
                }
                var delete = await _userManager.DeleteAsync(userExists);
                if (!delete.Succeeded)
                {
                    response.ErrorMessage = "Delete user failed! Try again!";
                    response.Success = false;
                    return response;
                }
                response.Data = true;
                response.Success = true;
                return response;
            }
            catch (Exception)
            {
                response.ErrorMessage = "Delete user failed! Try again!";
                response.Success = false;
                return response;
            }
        }

        public Response<IEnumerable<UserDto>> GetAllUsers()
        {
            var response = new Response<IEnumerable<UserDto>> { Data = new List<UserDto>() };
            try
            {
                var users = _userManager.Users.ToList();
                if (users == null)
                {
                    response.ErrorMessage = "No Users found!";
                    response.Success = false;
                    return response;
                }
                var usersDtos = users
                    .Select(x => new UserDto { UserName = x.UserName, Email = x.Email, Id = x.Id })
                    .ToList();
                response.Data = usersDtos;
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                response.Data = null;
                return response;
            }
        }

        public async Task<Response<UserDto>> GetUserById(string userId)
        {
            var response = new Response<UserDto> { Data = null };
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    response.ErrorMessage = "No such user found!";
                    response.Success = false;
                    return response;
                }

                var userDto = new UserDto()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Id = user.Id
                };

                response.Data = userDto;
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                return response;
            }
        }

        public async Task<Response<UserDto>> GetUserByUserName(string userName)
        {
            var response = new Response<UserDto> { Data = null };
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user == null)
                {
                    response.ErrorMessage = "No such user found!";
                    response.Success = false;
                    return response;
                }

                var userDto = new UserDto()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Id = user.Id
                };

                response.Data = userDto;
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                return response;
            }
        }

        public async Task<Response<List<RoleDto>>> GetUserRoles(string id)
        {
            var response = new Response<List<RoleDto>> { Data = new List<RoleDto>() };
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    response.ErrorMessage = "No such user found!";
                    response.Success = false;
                    return response;
                }
                var roles = await _userManager.GetRolesAsync(user);
                if (!roles.Any())
                {
                    response.ErrorMessage = "No roles associated with the user";
                    response.Success = true;
                    return response;
                }

                var roleDtos = roles.Select(x => new RoleDto() { RoleName = x }).ToList();

                response.Data = roleDtos;
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                response.Data = null;
                return response;
            }
        }

        public async Task<Response<UserDto>> UpdateAsync(UpdateUserDto user)
        {
            var response = new Response<UserDto> { Data = null };
            try
            {
                var userToUpdate = await _userManager.FindByIdAsync(user.Id.ToString().ToLower());
                if (userToUpdate == null)
                {
                    response.ErrorMessage = "No such user";
                    response.Success = false;
                    return response;
                }

                userToUpdate.FirstName = user.FirstName;
                userToUpdate.LastName = user.LastName;
                userToUpdate.DateOfBirth = user.DateOfBirth;

                var updateResponse = await _userManager.UpdateAsync(userToUpdate);
                if (!updateResponse.Succeeded)
                {
                    response.ErrorMessage = "Update failed!";
                    response.Success = false;
                    return response;
                }

                var updatedUser = await GetUserById(user.Id.ToString());
                if (!updatedUser.Success)
                {
                    response.Success = false;
                    response.ErrorMessage = "An error occurred while updating the user! Please try again!";
                    return response;
                }

                response.Success = true;
                response.Data = updatedUser.Data;
                return response;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Update of User has failed. Please try again:" + ex;
                response.Success = false;
                return response;
            }
        }
    }
}
