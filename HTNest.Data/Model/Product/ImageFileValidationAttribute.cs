using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HTNest.Data.Model.Product
{
    public class ImageFileValidationAttribute : ValidationAttribute
    {
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png" };
        private readonly long _maxSizeInBytes = 5 * 1024 * 1024; // 5MB

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file == null)
            {
                return new ValidationResult("Image file is required.");
            }

            // Kiểm tra kích thước tệp
            if (file.Length > _maxSizeInBytes)
            {
                return new ValidationResult("File size must be less than 5MB.");
            }

            // Kiểm tra phần mở rộng tệp
            var extension = System.IO.Path.GetExtension(file.FileName).ToLower();
            if (!_allowedExtensions.Contains(extension))
            {
                return new ValidationResult("Only JPG, JPEG, and PNG files are allowed.");
            }

            return ValidationResult.Success;
        }
    }
}
