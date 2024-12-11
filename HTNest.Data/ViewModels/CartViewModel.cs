using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Data.ViewModels
{
    public class CartViewModel
    {
        public string Message { get; set; }

        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }

        public double Price { get; set; }
        public string Description { get; set; }
        public double Discount { get; set; }

        public DateTime AddedDate { get; set; }
    }
    public class CartSummaryViewModel
    {
        public List<CartViewModel> CartItems { get; set; } = new List<CartViewModel>();
        public int TotalItems { get; set; }
        public double TotalPrice { get; set; }
    }
}
