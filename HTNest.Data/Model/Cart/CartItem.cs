using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Data.Model.Cart
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public DateTime AddedDate = DateTime.UtcNow.AddHours(7);
    }
}
