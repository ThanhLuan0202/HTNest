using HTNest.Data.Entities;
using HTNest.Data.Interfaces;
using HTNest.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        public Task<Category> AddAsync(Category category)
        {
            return _categoryRepository.AddAsync(category);
        }

        public Task<Category> Delete(int id)
        {
            return _categoryRepository.Delete(id);
        }

        public Task<IEnumerable<Category>> GetAllAsync(string? categoryName)
        {
            return _categoryRepository.GetAllAsync(categoryName);
        }

        public Task<Category> GetByIdAsync(int id)
        {
            return _categoryRepository.GetById(id);
        }

        public Task<Category> UpdateAsync(Category category, int id)
        {
            return _categoryRepository.UpdateAsync(category, id);
        }
    }
}
