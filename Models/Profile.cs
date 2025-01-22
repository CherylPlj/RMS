using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models
{
    public class Profile
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        //public string? Unit { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ProfilePicture { get; set; }
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmPassword { get; set; }

        //public int UserId { get; set; } // Foreign key
       //public virtual User User { get; set; }
        // [NotMapped] // EF Core will ignore this property
        // public IFormFile? ProfilePic { get; set; }
    }
}