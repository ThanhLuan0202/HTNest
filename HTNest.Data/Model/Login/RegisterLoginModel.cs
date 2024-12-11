using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Data.Model.Login
{
    public class RegisterLoginModel
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public DateTime DOB { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string PhoneNumber { get; set; }
        public string Status = "Active";
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(7);



    }
}
