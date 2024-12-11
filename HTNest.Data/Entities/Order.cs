using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Data.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string Status { get; set; }
        public double TotalPrice { get; set; }
        public string UserName { get; set; }

        public DateTime OrderDate { get; set; }
        [ForeignKey("UserName")]
        public virtual User User { get; set; }

        public virtual ICollection<OrderDetail> OrderDetail { get; set; }

    }
}
