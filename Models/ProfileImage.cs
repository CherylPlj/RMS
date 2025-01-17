using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models
{
    public class ProfileImage
    {
        [Key]
        public int PImageId { get; set; } // Primary key for the ProfileImage table

        [ForeignKey(nameof(User))]
        public int? UserID { get; set; } // Foreign key referencing the User entity

        public string? PFilePath { get; set; } // File path of the uploaded profile image

        // Navigation property for the related User
        public virtual User? User { get; set; }
    }
}
