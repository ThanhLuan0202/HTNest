using HTNest.Data.Entities;
using HTNest.Data.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Data.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll(string? productName);
        Task<Product> GetById(int id);
        Task<Product> Add(CreateProductModel product);
        Task<Product> Update(int id, UpdateProductModel product);
        Task<Product> Delete(int id);

    }
}
