using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Data.Model.Category
{
    public class CreateCategoryModel
    {
        [Required]
        [MinLength(3, ErrorMessage = "CategoryName least 3 character")]
        public string? categoryName { get; set; }
        public string? Status = "Active";



    }
}
