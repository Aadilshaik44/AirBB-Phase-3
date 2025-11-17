using Microsoft.EntityFrameworkCore;
using System.Linq; // Add this using directive if it's not present

namespace AirBB.Models
{
    public class AirBnbContext : DbContext
    {
        public AirBnbContext(DbContextOptions<AirBnbContext> options) : base(options) { }

        // Existing DbSet declarations...
        public DbSet<Location> Locations { get; set; } = null!;
        public DbSet<Residence> Residences { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;
        
        // NEW DbSet from Phase 3
        public DbSet<Client> Clients { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // --- 1. SEED CLIENTS (NEW FOR PHASE 3) ---
            modelBuilder.Entity<Client>().HasData(
                new Client { UserId = 1, Name = "Admin Owner", Email = "owner@airbb.com", UserType = "Owner" },
                new Client { UserId = 2, Name = "Test Client", Email = "client@airbb.com", UserType = "Client" }
            );


            // --- 2. SEED LOCATIONS (EXISTING) ---
            modelBuilder.Entity<Location>().HasData(
                new Location { LocationId = 1, Name = "Chicago" },
                new Location { LocationId = 2, Name = "New York" },
                new Location { LocationId = 3, Name = "Boston" },
                new Location { LocationId = 4, Name = "Miami" }
            );

            // --- 3. SEED RESIDENCES (UPDATE OwnerId TO 1) ---
            modelBuilder.Entity<Residence>().HasData(
                // Note: OwnerId is now 1 for all properties. BuiltYear defaults to 0, which is fine for now.
                new Residence { ResidenceId = 101, Name = "Chicago Loop Apartment", ResidencePicture = "chi_loop.jpg", LocationId = 1, OwnerId = 1, GuestNumber = 4, BedroomNumber = 2, BathroomNumber = 1.0, PricePerNight = 189.00M, BuiltYear = 2000 },
                new Residence { ResidenceId = 102, Name = "Lincoln Park Flat", ResidencePicture = "chi_lincoln.jpg", LocationId = 1, OwnerId = 1, GuestNumber = 3, BedroomNumber = 1, BathroomNumber = 1.0, PricePerNight = 139.00M, BuiltYear = 1985 },
                new Residence { ResidenceId = 201, Name = "NYC Soho Loft", ResidencePicture = "nyc_soho.jpg", LocationId = 2, OwnerId = 1, GuestNumber = 2, BedroomNumber = 1, BathroomNumber = 1.0, PricePerNight = 259.00M, BuiltYear = 2010 },
                new Residence { ResidenceId = 301, Name = "Boston Back Bay Condo", ResidencePicture = "bos_backbay.jpg", LocationId = 3, OwnerId = 1, GuestNumber = 4, BedroomNumber = 2, BathroomNumber = 2.0, PricePerNight = 209.00M, BuiltYear = 1995 },
                new Residence { ResidenceId = 401, Name = "Miami Beach House", ResidencePicture = "mia_beach.jpg", LocationId = 4, OwnerId = 1, GuestNumber = 6, BedroomNumber = 3, BathroomNumber = 2.0, PricePerNight = 299.00M, BuiltYear = 2015 }
            );

            // --- 4. SEED RESERVATIONS (EXISTING) ---
            // Ensure this is run after Residences are seeded
            // ... (Your existing Reservation seed data remains here)
            
            // Call the base implementation last
            base.OnModelCreating(modelBuilder);
        }
    }
}