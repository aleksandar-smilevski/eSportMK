using System.Collections.Generic;
using eSportMK.Data;
using eSportMK.Dto;

namespace eSportMk.Services
{
    public interface IUserService
    {
        bool AssignUserToRole(string userId, Role role);
        bool UserNameExists(string userName);
        User Create(CreateUserDto userDto);
        bool Delete(string userId);
        IEnumerable<User> GetAllUsers();
        User GetUserById(string userId);
        User GetUserByUserName(string userName);
        Role GetUserRole(string userId);
    }
}