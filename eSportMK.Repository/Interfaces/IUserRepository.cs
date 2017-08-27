using eSportMK.Data;
using eSportMK.Dto;
using eSportMK.Repository.ResponseObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eSportMK.Repository.Interfaces
{
    public interface IUserRepository
    {
        Response<IEnumerable<UserDto>> GetAllUsers();
        Task<Response<UserDto>> GetUserByUserName(string userName);
        Task<Response<UserDto>> GetUserById(string userId);
        Response<UserDto> Create(CreateUserDto user);
        Task<Response<UserDto>> UpdateAsync(UpdateUserDto user);
        Task<Response<bool>> Delete(string userId);
        Response<bool> AssignUserToRole(string userId, RoleDto role);
        Response<bool> UserNameExists(string userName);
        Task<Response<List<RoleDto>>> GetUserRoles(string userId);
    }
}
