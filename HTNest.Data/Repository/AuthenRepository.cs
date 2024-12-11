using AutoMapper;
using HTNest.Data.Data;
using HTNest.Data.Entities;
using HTNest.Data.Interfaces;
using HTNest.Data.Model.Login;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Data.Repository
{
    public class AuthenRepository : Repository<User>, IAuthenRepository
    {

        private readonly HTNestDbContext _DbContext;
        private readonly IConfiguration configuration;
        private readonly IMapper _mapper;

        public AuthenRepository(HTNestDbContext DbContext, IConfiguration configuration) : base(DbContext)
        {
            _DbContext = DbContext;
            this.configuration = configuration;
        }

        public string CreateJWTToken(User user, string roles)
        {
            var claims = new[]
            {
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.Role, user.Role.RoleName),
                    new Claim(ClaimTypes.Email, user.Email)
            };



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    configuration["Jwt:Issuer"],
                    configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> Login(LoginModel model)
        {
            var user = await Entities
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Status == "Active" && x.Email.ToLower().Equals(model.Email));

            if (user == null)
            {
                throw new Exception($"User not existed !!");
            }

            if (model.Password != user.Password)
            {
                throw new Exception($"Password wrong !!");
            }

            var claims = new[]
            {
        new Claim(ClaimTypes.Name, user.FullName),
        new Claim(ClaimTypes.Role, user.Role.RoleName),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.NameIdentifier, user.UserName.ToString()),
        new Claim("Image", user.Image != null ? user.Image.ToString() : string.Empty)
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public async Task<User> Register(User newUser)
        {
            var checkEmail = await Entities.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(newUser.Email));
            if (checkEmail != null)
            {
                throw new Exception($"Email is existed !!");
            }
            var checkPhoneNumber = await Entities.FirstOrDefaultAsync(x => x.PhoneNumber.Equals(newUser.PhoneNumber));
            if (checkPhoneNumber != null)
            {
                throw new Exception($"Phonenumber is existed !!");
            }
            var checkUserName = await Entities.FirstOrDefaultAsync(x => x.UserName.Equals(newUser.UserName));
            if (checkUserName != null)
            {
                throw new Exception($"UserName is existed !!");
            }
            await Entities.AddAsync(newUser);
            await _DbContext.SaveChangesAsync();
            return newUser;
        }
        public async Task<bool> Logout(string userId)
        {
            return await Task.FromResult(true);
        }
    }
}
