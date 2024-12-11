using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Data.ViewModels
{
    public class ProductViewModel
    {
        public string ProductCode { get; set; }
        public string? Image { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Ingredient { get; set; }
        public double Discount { get; set; }

        public string Warning { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
        public int StyleId { get; set; }
        public string Status { get; set; }
        public double Price { get; set; }
        public DateTime CreateTime { get; set; }
        public double Volume { get; set; }
    }
}
