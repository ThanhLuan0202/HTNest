using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Data.Entities
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice    { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public  Order Order { get; set; }

        [ForeignKey("ProductId")]
        public  Product Product { get; set; }

    }
}
