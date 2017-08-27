using System;
using System.Collections.Generic;
using eSportMK.Data;
using eSportMK.Dto;
using eSportMK.Repository.Interfaces;

namespace eSportMk.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool AssignUserToRole(string userId, Role role) 
            => _userRepository.AssignUserToRole(userId, role).Data;

        public bool UserNameExists(string userName)
            => _userRepository.UserNameExists(userName).Data;

        public User Create(CreateUserDto userDto)
            => _userRepository.Create(userDto).Data;

        public bool Delete(string userId)
            => _userRepository.Delete(userId).Data;

        public IEnumerable<User> GetAllUsers()
            => _userRepository.GetAllUsers().Data;

        public User GetUserById(string userId)
            => _userRepository.GetUserById(userId).Data;

        public User GetUserByUserName(string userName)
            => _userRepository.GetUserByUserName(userName).Data;

        public Role GetUserRole(string userId)
            => _userRepository.GetUserRole(userId).Data;
    }
}
