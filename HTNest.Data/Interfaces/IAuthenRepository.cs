using HTNest.Data.Entities;
using HTNest.Data.Model.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Data.Interfaces
{
    public interface IAuthenRepository
    {
        Task<string> Login(LoginModel model);
        Task<User> Register(User newUser);
        string CreateJWTToken(User user, string roles);
        Task<bool> Logout(string userId);
    }
}
