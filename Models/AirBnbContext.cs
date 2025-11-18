using Microsoft.EntityFrameworkCore;
using System.Linq; 

namespace AirBB.Models
{
    public class AirBnbContext : DbContext
    {
        public AirBnbContext(DbContextOptions<AirBnbContext> options) : base(options) { }

        
        public DbSet<Location> Locations { get; set; } = null!;
        public DbSet<Residence> Residences { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;
        
        
        public DbSet<Client> Clients { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Client>().HasData(
                new Client { UserId = 1, Name = "Admin Owner", Email = "owner@airbb.com", UserType = "Owner" },
                new Client { UserId = 2, Name = "Test Client", Email = "client@airbb.com", UserType = "Client" }
            );


            
            modelBuilder.Entity<Location>().HasData(
                new Location { LocationId = 1, Name = "Chicago" },
                new Location { LocationId = 2, Name = "New York" },
                new Location { LocationId = 3, Name = "Boston" },
                new Location { LocationId = 4, Name = "Miami" }
            );

            
            modelBuilder.Entity<Residence>().HasData(
                
                new Residence { ResidenceId = 101, Name = "Chicago Loop Apartment", ResidencePicture = "chi_loop.jpg", LocationId = 1, OwnerId = 1, GuestNumber = 4, BedroomNumber = 2, BathroomNumber = 1.0, PricePerNight = 189.00M, BuiltYear = 2000 },
                new Residence { ResidenceId = 102, Name = "Lincoln Park Flat", ResidencePicture = "chi_lincoln.jpg", LocationId = 1, OwnerId = 1, GuestNumber = 3, BedroomNumber = 1, BathroomNumber = 1.0, PricePerNight = 139.00M, BuiltYear = 1985 },
                new Residence { ResidenceId = 201, Name = "NYC Soho Loft", ResidencePicture = "nyc_soho.jpg", LocationId = 2, OwnerId = 1, GuestNumber = 2, BedroomNumber = 1, BathroomNumber = 1.0, PricePerNight = 259.00M, BuiltYear = 2010 },
                new Residence { ResidenceId = 301, Name = "Boston Back Bay Condo", ResidencePicture = "bos_backbay.jpg", LocationId = 3, OwnerId = 1, GuestNumber = 4, BedroomNumber = 2, BathroomNumber = 2.0, PricePerNight = 209.00M, BuiltYear = 1995 },
                new Residence { ResidenceId = 401, Name = "Miami Beach House", ResidencePicture = "mia_beach.jpg", LocationId = 4, OwnerId = 1, GuestNumber = 6, BedroomNumber = 3, BathroomNumber = 2.0, PricePerNight = 299.00M, BuiltYear = 2015 }
            );

        
            base.OnModelCreating(modelBuilder);
        }
    }
}