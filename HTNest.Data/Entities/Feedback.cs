using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Data.Entities
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }
        public int Rating { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public string? Comment {  get; set; }
        public DateTime? FeedbackTime { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("UserName")]
        public virtual User User { get; set; }
        [ForeignKey("ProductId")] 
        public virtual Product Product { get; set; }
    }
}
