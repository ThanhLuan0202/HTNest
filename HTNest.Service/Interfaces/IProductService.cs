using HTNest.Data.Entities;
using HTNest.Data.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Service.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll(string? productName);
        Task<Product> GetById(int id);
        Task<Product> Add(CreateProductModel product);
        Task<Product> Update(int id, UpdateProductModel product); 
        Task<Product> DeleteById(int id);
    } 
}
