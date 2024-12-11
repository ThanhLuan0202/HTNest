using HTNest.Data.Entities;
using HTNest.Data.Interfaces;
using HTNest.Data.Model.User;
using HTNest.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public Task<User> Add(CreateUserModel newUser)
        {
           return _userRepository.Add(newUser);
        }

        public Task<User> Delete(string userName)
        {
            return _userRepository.Delete(userName);
        }

        public Task<IEnumerable<User>> GetAll()
        {
            return _userRepository.GetAll();
        }

        public Task<User> GetByUserName(string userName)
        {
            return _userRepository.GetByUserName(userName);
        }

        public Task<User> Update(string userName, User newUser)
        {
            return _userRepository.Update(userName, newUser);
        }
    }
}
