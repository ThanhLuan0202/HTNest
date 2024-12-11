using HTNest.Data.Entities;
using HTNest.Data.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Service.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> Add(CreateUserModel newUser);

        Task<User> Update(string userName, User newUser);
        Task<User> Delete(string userName);
        Task<User> GetByUserName(string userName);
    }
}
