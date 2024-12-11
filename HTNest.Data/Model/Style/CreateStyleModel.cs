using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Data.Model.Style
{
    public class CreateStyleModel
    {
        [Required]
        [MinLength(1, ErrorMessage = "StyleName must be least 3 character!!")]
        public string? StyleName { get; set; }
        public string? Status = "Active";
    }
}
