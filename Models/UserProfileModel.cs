using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

public class UserProfileModel
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Unit { get; set; }

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
}
