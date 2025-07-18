﻿using HTNest.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Data.ViewModels
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public string? FullName { get; set; }
        public DateTime DOB { get; set; }
        public string? Image { get; set; }

        public string? Password { get; set; }
        public string? Email { get; set; }
        public int RoleId { get; set; }
        public string PhoneNumber { get; set; }
        public string Status { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
