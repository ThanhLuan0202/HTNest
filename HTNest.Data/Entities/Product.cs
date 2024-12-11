using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Data.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string? Image { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
        public int  StyleId { get; set; }
        public string Status { get; set; }
        public double Price { get; set; }
        public string Ingredient { get; set; }
        public double Discount { get; set; }

        public string Warning { get; set; }
        public DateTime CreateTime { get; set; }
        public double Volume { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
       
        [ForeignKey("CategoryId")]
        public  Category Category { get; set; }
        [ForeignKey("StyleId")]
        public Style Style { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
