﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models
{
    public class Staff
    {
        [Key]
        public int StaffID { get; set; } // Primary Key

        [ForeignKey(nameof(User))]
        public int? UserId { get; set; }
        public string? PhoneNumber { get; set; } // Staff phone number
        public string? StaffRole { get; set; } // Staff role (e.g., Maintenance, Admin, etc.
        public virtual User? User { get; set; }

        // Navigation properties can be added if there are relationships, e.g., Maintenance Requests assigned to the staff
        public virtual ICollection<MaintenanceRequest>? MaintenanceRequests { get; set; }
    }
}
