using HTNest.Data.Entities;
using HTNest.Data.Interfaces;
using HTNest.Data.Model.Login;
using HTNest.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Service.Services
{

    public class AuthenService : IAuthenService
    {
        private readonly IAuthenRepository _authenRepository;

        public AuthenService(IAuthenRepository authenRepository)
        {
            _authenRepository = authenRepository;
        }

        public string CreateJWTToken(User user, string role )
        {
            return _authenRepository.CreateJWTToken(user, role);
        }

        public Task<string> Login(LoginModel loginModel)
        {
            return _authenRepository.Login(loginModel);
        }

        public Task<User> Register(User newUser)
        {
            return _authenRepository.Register(newUser);
        }
        public async Task<bool> Logout(HttpContext httpContext)
        {
            httpContext.Response.Cookies.Delete("authToken");
            return await Task.FromResult(true);
        }

        public bool IsTokenBlacklisted(string token)
        {
            return false;
        }
    }
}
