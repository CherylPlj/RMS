using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RMS.Models;

namespace RMS 
{
    public class AppDbContext : DbContext
    {
        public DbSet<Unit> Units { get; set; } // This matches your model

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("DefaultConnection")
                .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Unit>().HasData(
                new Unit { Id = 1, Name = "Cozy Suite 10", Price = 11000, Status = "Occupied" },
                new Unit { Id = 2, Name = "Cozy Suite 11", Price = 12000, Status = "Occupied" },
                new Unit { Id = 3, Name = "Cozy Suite 12", Price = 13000, Status = "Available" },
                new Unit { Id = 4, Name = "Cozy Suite 13", Price = 14000, Status = "Under Maintenance" },
                new Unit { Id = 5, Name = "Cozy Suite 14", Price = 15000, Status = "Available" }
            );
        }

    }
}
