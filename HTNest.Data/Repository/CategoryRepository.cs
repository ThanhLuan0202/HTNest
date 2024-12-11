using HTNest.Data.Data;
using HTNest.Data.Entities;
using HTNest.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        readonly HTNestDbContext _DbContext;
        public CategoryRepository(HTNestDbContext dbContext ) : base(dbContext)
        {
            this._DbContext = dbContext;
        }

        public async Task<Category> AddAsync(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            var existCategory = await Entities.FirstOrDefaultAsync(x => x.CategoryName.Equals(category.CategoryName));
            if (existCategory != null)
            {
                throw new Exception($"Category {category.CategoryName} is existed !!");
            }
            await Entities.AddAsync(category);
            await _DbContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category> Delete(int id)
        {
            var cate = Entities.FirstOrDefault(x => x.CategoryID == id);
            if (cate != null)
            {
                cate.Status = "Inactive";
            }
            _DbContext.SaveChanges();
            return cate;
        }

        public async Task<IEnumerable<Category>> GetAllAsync(string? categoryName)
        {
            var query = Entities.Where(x => x.Status.ToLower() == "Active");
            if (!string.IsNullOrEmpty(categoryName))
            {
                query = Entities.Where(x => x.CategoryName.ToLower().Contains(categoryName.ToLower()));
            }
            return await query.ToListAsync();
        }

        public async Task<Category> GetById(int id)
        {
            var cate = Entities.FirstOrDefaultAsync(x => x.Status.ToLower() == "Active" && x.CategoryID == id );
            return await cate;
        }
   

        public async Task<Category> UpdateAsync(Category category, int id)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));

            }
            var existCategory = await GetById(id);
            if (existCategory == null)
            {
                throw new KeyNotFoundException($"Category with Id {id} not found.");

            }
            existCategory.CategoryName = category.CategoryName;
            _DbContext.SaveChanges();
            return existCategory;
        }

      
    }
}
