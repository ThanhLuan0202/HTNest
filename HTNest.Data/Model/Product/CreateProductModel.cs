using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Data.Model.Product
{
    public class CreateProductModel
    {

        public string ProductCode { get; set; }
        [Required(ErrorMessage = "Image file is required.")]

        [ImageFileValidation]
        public IFormFile? Image { get; set; }
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        public double Discount { get; set; }

        public string Ingredient { get; set; }
        public string Warning { get; set; }
        [Required(ErrorMessage = "Category is required.")]

        public int CategoryId { get; set; }
        public int StyleId { get; set; }
        public string Status = "Active";
        [Required(ErrorMessage = "Price is required.")]
        [Range(10, double.MaxValue, ErrorMessage = "Price must be at least $10.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Price must be a valid number and cannot contain special characters.")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Create date is required.")]

        public DateTime CreateTime = DateTime.UtcNow.AddHours(7);
        public double Volume { get; set; }


    }
}
