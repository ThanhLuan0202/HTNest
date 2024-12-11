using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Data.Model.Product
{
    public class UpdateProductModel
    {

        public IFormFile? Image { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        public double Discount { get; set; }

        public string Ingredient { get; set; }
        public string Warning { get; set; }
        public int CategoryId { get; set; }
        public int StyleId { get; set; }
        public string Status = "Active";
        public double Price { get; set; }
        public DateTime CreateTime = DateTime.UtcNow.AddHours(7);
        public double Volume { get; set; }

    }
}
