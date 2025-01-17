using System.ComponentModel.DataAnnotations;

namespace RMS.Models
{
    public class EditProfileViewModel
    {
        [Required]
        public int UserID { get; set; } // Primary Key
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        public IFormFile ProfilePicture { get; set; } // For file uploads

        // public string ProfilePicturePath { get; set; } // Optional for displaying current picture
        public virtual ICollection<UnitImage>? Images { get; set; }
    }
}
