using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Data.Model.User
{
    public class UpdateUserModel
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int RoleId { get; set; }
        public string Status = "Active";
        public DateTime CreateDate = DateTime.UtcNow.AddHours(7);
    }
}
