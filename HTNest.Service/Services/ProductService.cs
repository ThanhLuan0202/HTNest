using HTNest.Data.Entities;
using HTNest.Data.Interfaces;
using HTNest.Data.Model.Product;
using HTNest.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public Task<Product> Add(CreateProductModel product)
        {
            return _repo.Add(product);
        }

        public Task<Product> DeleteById(int id)
        {
            return _repo.Delete(id);
        }

        public Task<IEnumerable<Product>> GetAll(string? productName)
        {
            return _repo.GetAll(productName);
        }

        public Task<Product> GetById(int id)
        {
            return _repo.GetById(id);
        }

        public Task<Product> Update(int id, UpdateProductModel product)
        {
            return _repo.Update(id, product);
        }
    }
}
