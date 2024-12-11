using AutoMapper;
using Firebase.Storage;
using HTNest.Data.Data;
using HTNest.Data.Entities;
using HTNest.Data.Interfaces;
using HTNest.Data.Model.User;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly HTNestDbContext _DbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration configuration;

        public UserRepository(HTNestDbContext DbContext, IMapper mapper, IConfiguration configuration) : base(DbContext)
        {
            _DbContext = DbContext;
            _mapper = mapper;
            this.configuration = configuration;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var query = Entities.Where(x => x.Status.ToLower().Equals("Active"));

            return await query.ToListAsync();
        }

        public async Task<User> Add(CreateUserModel newUser)
        {
            var checkUserName = Entities.FirstOrDefault(x => x.UserName.ToLower().Contains(newUser.UserName));
            if (checkUserName != null)
            {
                throw new Exception($"UserName {newUser.UserName} is existed !!");

            }
            var checkEmail = Entities.FirstOrDefault(x => x.Email.ToLower().Contains(newUser.Email));
            if (checkEmail != null)
            {
                throw new Exception($"Email {newUser.Email} is existed !!");
            }
            var checkPhoneNumber = Entities.FirstOrDefault(x => x.PhoneNumber.Contains(newUser.PhoneNumber));
            if (checkPhoneNumber != null)
            {
                throw new Exception($"Phone number {newUser.PhoneNumber} is existed !!");
            }

            var user = new User
            {
                Email = newUser.Email,
                CreateDate = newUser.CreateDate,
                DOB = newUser.DOB,
                FullName = newUser.FullName,
                Password = newUser.Password,
                Status = newUser.Status,
                PhoneNumber = newUser.PhoneNumber,
                RoleId = newUser.RoleId,
                UserName = newUser.UserName,

            };
            if (newUser.Image != null)
            {
                var imageUrl = await UploadFileAsyncc(newUser.Image);
                user.Image = imageUrl;
            }
            await Entities.AddAsync(user);
            await _DbContext.SaveChangesAsync();
            return user;
        }
        public async Task<string> UploadFileAsyncc(IFormFile file)
        {
            if (file.Length > 0)
            {
                var stream = file.OpenReadStream();
                var bucket = configuration["FireBase:Bucket"];

                var task = new FirebaseStorage(bucket)
                    .Child("Image_Course")
                    .Child(file.FileName)
                    .PutAsync(stream);

                var downloadUrl = await task;
                return downloadUrl;
            }
            return null;
        }

        public async Task<User> Update(string userName, User newUser)
        {
            var existUser = Entities.FirstOrDefault(x => x.UserName.ToLower().Contains(userName));
            if (existUser == null)
            {
                throw new Exception($"User not existed !!");

            }
            var checkEmail = Entities.FirstOrDefault(x => x.Email.ToLower().Contains(newUser.Email));
            if (checkEmail != null)
            {
                throw new Exception($"Email  {newUser.Email} is existed !!");
            }
            var checkPhoneNumber = Entities.FirstOrDefault(x => x.PhoneNumber.Equals(newUser.PhoneNumber));
            if (checkPhoneNumber != null)
            {
                throw new Exception($"Phone number {newUser.PhoneNumber} is existed !!");
            }

            existUser.Email = newUser.Email;
            existUser.PhoneNumber = newUser.PhoneNumber;
            await _DbContext.SaveChangesAsync();
            return existUser;
        }

        public async Task<User> Delete(string userName)
        {
            var checkUser = Entities.FirstOrDefault(x => x.UserName.Contains(userName));
            if (checkUser == null)
            {
                throw new Exception($"User not existed !!");

            }
            checkUser.Status = "Inactive";
            await _DbContext.SaveChangesAsync();    
            return checkUser;
        }

        public async Task<User> GetByUserName(string userName)
        {
            var query = Entities.FirstOrDefault( x => x.UserName.Contains(userName) && x.Status.Equals("Active"));
            if (query == null)
            {
                throw new Exception($"User not existed !!");
            }
            return query;
        }
    }
}
