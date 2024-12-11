using HTNest.Data.Entities;
using HTNest.Data.Model.Product;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace HTNest.Data.Model.User
{
    public class CreateUserModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [ImageFileValidation]
        public IFormFile? Image { get; set; }

        [Required]
        public int RoleId { get; set; }

        public string Status { get; set; } = "Active";
        public DateTime CreateDate { get; set; } = DateTime.UtcNow.AddHours(7);
        public DateTime DOB = DateTime.UtcNow.AddHours(7);
        public string? FullName { get; set; }


    }
}
