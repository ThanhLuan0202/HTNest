using HTNest.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Service.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync(string? categoryName);
        Task<Category> GetByIdAsync(int id);
        Task<Category> Delete(int id);
        Task<Category> AddAsync(Category category);
        Task<Category> UpdateAsync(Category category, int id);
        
    }
}
