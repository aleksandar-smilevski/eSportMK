using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSportMK.Data;
using eSportMK.Database;
using eSportMK.Repository.Interfaces;
using eSportMK.Repository.ResponseObject;
using Microsoft.EntityFrameworkCore;

namespace eSportMK.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _db;

        public RoleRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Response<IEnumerable<Role>> GetAllRoles()
        {
            var response = new Response<IEnumerable<Role>> { Data = null };
            var roles = _db.Roles.ToList();
            if (roles == null)
            {
                response.ErrorMessage = "No Roles found!";
                response.Success = false;
                return response;
            }
            response.Data = roles;
            response.Success = true;
            return response;
        }

        public Response<Role> GetRoleByName(string roleName)
        {
            var response = new Response<Role> { Data = null };
            var role = _db.Roles.FirstOrDefault(x => x.Name.Equals(roleName));
            if (role == null)
            {
                response.ErrorMessage = "No such role found!";
                response.Success = false;
                return response;
            }
            response.Data = role;
            response.Success = true;
            return response;
        }

        public Response<Role> GetRoleById(string roleId)
        {
            var response = new Response<Role> { Data = null };
            var role = _db.Roles.FirstOrDefault(x => x.Id.ToString().Equals(roleId));
            if (role == null)
            {
                response.ErrorMessage = "No such user found!";
                response.Success = false;
                return response;
            }
            response.Data = role;
            response.Success = true;
            return response;
        }

        public Response<Role> Create(Role role)
        {
            var response = new Response<Role> { Data = null };
            var roleExists = GetRoleByName(role.Name);
            if (roleExists.Data != null)
            {
                response.ErrorMessage = "Role already exists";
                response.Success = false;
                return response;
            }
            try
            {
                var newRole = new Role()
                {
                    Name  = role.Name,
                    Users = role.Users ?? new List<User>()
                };
                _db.Roles.Add(newRole);
                _db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                response.ErrorMessage = "Failed to create role";
                response.Success = false;
                return response;
            }
            var addedRole = GetRoleByName(role.Name);
            if (addedRole.Data == null)
            {
                response.ErrorMessage = "Failed to create role";
                response.Success = false;
                return response;
            }
            response.Data = addedRole.Data;
            response.Success = true;
            return response;
        }

        public Task<Response<Role>> UpdateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public Response<bool> Delete(string roleId)
        {
            var response = new Response<bool> { Data = false };
            var roleExists = GetRoleById(roleId);
            if (roleExists.Data == null)
            {
                response.ErrorMessage = "Role does not exist";
                response.Success = false;
                return response;
            }

            var role = roleExists.Data;

            if (role.Users.Any())
            {
                response.ErrorMessage = "Cannot delete role, since there are Users assigned to it";
                response.Success = false;
                return response;
            }
            try
            {
                _db.Roles.Remove(role);
                _db.SaveChanges();
                response.Data = true;
                response.Success = true;
                return response;
            }

            catch (DbUpdateException)
            {
                response.ErrorMessage = "Delete role failed! Try again!";
                response.Success = false;
                return response;
            }
        }

        public Response<IEnumerable<User>> GetAllUsersInRole(string roleId)
        {
            var response = new Response<IEnumerable<User>> { Data = null };
            var getRole = GetRoleById(roleId);
            if (getRole.Data == null)
            {
                response.ErrorMessage = "No such role found!";
                response.Success = false;
                return response;
            }

            var role = getRole.Data;

            if (!role.Users.Any())
            {
                response.ErrorMessage = "Role has no users assigned to it";
                response.Success = false;
                return response;
            }

            response.Data = role.Users;
            response.Success = true;
            return response;
        }
    }
}
