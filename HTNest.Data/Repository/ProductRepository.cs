using AutoMapper;
using Firebase.Storage;
using HTNest.Data.Data;
using HTNest.Data.Entities;
using HTNest.Data.Interfaces;
using HTNest.Data.Model.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;


namespace HTNest.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly HTNestDbContext _DbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration configuration;

        public ProductRepository(HTNestDbContext DbContext, IMapper mapper, IConfiguration configuration) : base(DbContext)
        {
            _DbContext = DbContext;
            _mapper = mapper;
            this.configuration = configuration;
        }

        public async Task<Product> Add(CreateProductModel product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));

            }
            var existProduct = await Entities.FirstOrDefaultAsync(x => x.ProductCode.Equals(product.ProductCode));
          
            if (existProduct != null)
            {
                throw new Exception($"Product {product.ProductCode} is existed !!");
            }
            var newProduct = new Product
            {
                ProductCode = product.ProductCode,
                ProductName = product.ProductName,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Description = product.Description,
                Discount = product.Discount,
                Status = product.Status,
                CreateTime = DateTime.Now.AddHours(7),
                Ingredient = product.Ingredient,
                Warning = product.Warning,
                StyleId = product.StyleId,
                StockQuantity = product.StockQuantity,
                Volume = product.Volume,
            };
            if (product.Image != null)
            {
                var imageUrl = await UploadFileAsyncc(product.Image);
                newProduct.Image = imageUrl;
            }
            await Entities.AddAsync(newProduct);
            await _DbContext.SaveChangesAsync();
            return newProduct;

        }

        public async Task<Product> Delete(int id)
        {
            var existProduct = Entities.FirstOrDefault(x => x.ProductId.Equals(id) && x.Status == "Active");
            if (existProduct == null)
            {
                throw new Exception($"Product {id} is not existed !!");
            }
            existProduct.Status = "Inactive";
            await _DbContext.SaveChangesAsync();
            return existProduct;

        }

        public async Task<IEnumerable<Product>> GetAll(string? productName)
        {
            var query = Entities.Where(x => x.Status.ToLower() == "Active");

            if (!string.IsNullOrEmpty(productName))
            {
                query = Entities.Where(x => x.ProductName.ToLower().Contains(productName));
            }
            return await query.ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            var query = Entities.FirstOrDefaultAsync(x => x.ProductId == id && x.Status.ToLower() == "Active");
            return await query;

          
        }

        public async Task<Product> Update(int id, UpdateProductModel product)
        {
            var existProduct = await GetById(id);

            if (existProduct == null)
            {
                throw new Exception($"Product {id} is not existed !!");

            }
           
            if (product.Image != null)
            {
                var imageUrl = await UploadFileAsyncc(product.Image);
                existProduct.Image = imageUrl;
            }
            existProduct.ProductName = product.ProductName;
            existProduct.CategoryId = product.CategoryId;
            existProduct.StyleId = product.StyleId;
            existProduct.Description = product.Description;
            existProduct.Volume = product.Volume;
            existProduct.Price = product.Price;
            existProduct.StockQuantity = product.StockQuantity;
            existProduct.Discount = product.Discount;
            
            _DbContext.SaveChanges();
            return existProduct;

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
    }
}
