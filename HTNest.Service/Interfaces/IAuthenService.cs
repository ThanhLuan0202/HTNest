using HTNest.Data.Entities;
using HTNest.Data.Model.Login;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Service.Interfaces
{
    public interface IAuthenService
    {
        Task<string> Login(LoginModel loingModel);
        Task<User> Register(User newUser);
        string CreateJWTToken(User user, string roles);
        Task<bool> Logout(HttpContext httpContext);
        bool IsTokenBlacklisted(string token);
    }
}
