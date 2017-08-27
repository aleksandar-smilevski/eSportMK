using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using eSportMK.Data;
using eSportMK.Dto;
using eSportMK.Repository.ResponseObject;

namespace eSportMK.Repository.Interfaces
{
    public interface IRoleRepository
    {
        Response<IEnumerable<Role>> GetAllRoles();
        Response<Role> GetRoleByName(string roleName);
        Response<Role> GetRoleById(string roleId);
        Response<Role> Create(Role role);
        Task<Response<Role>> UpdateRole(string roleName);
        Response<bool> Delete(string roleId);
        Response<IEnumerable<User>> GetAllUsersInRole(string roleId);
    }
}
