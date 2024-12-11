using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Data.Entities
{
    public class Style
    {
        [Key]
        public int StyleId { get; set; }
        public string? StyleName { get; set; }
        public string? Status { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        
    }
}
