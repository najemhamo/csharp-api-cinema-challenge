using Microsoft.EntityFrameworkCore;
using api_cinema_challenge.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using api_cinema_challenge.UserRoles;
using Microsoft.AspNetCore.Identity;

namespace api_cinema_challenge.Data
{
    public class CinemaContext : IdentityUserContext<ApplicationUser>
    {
        public CinemaContext(DbContextOptions<CinemaContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            DateTime utc = DateTime.Now.ToUniversalTime();
            //modelBuilder.Entity<Screening>().HasKey(s => new {s.Id});
         

            // SEED Costumers
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "Chris Wolstenholme", Email = "chris@muse.mu", Phone = "+44729388192", CreatedAt = utc, UpdatedAt = utc },
                new Customer { Id = 2, Name = "Max Peter", Email = "max.peter@gmail.com", Phone = "+49123456789", CreatedAt = utc, UpdatedAt = utc }
            );

            // SEED Movies
            modelBuilder.Entity<Movie>().HasData(
                new Movie { Id = 1, Title = "Dodgeball", Rating = "PG-13", Description = "The greatest movie ever made.", RunTimeMinutes = 126, CreatedAt = utc, UpdatedAt = utc },
                new Movie { Id = 2, Title = "The Matrix", Rating = "R", Description = "The greatest movie ever made.", RunTimeMinutes = 126, CreatedAt = utc, UpdatedAt = utc }
            );

            // SEED Screenings
            modelBuilder.Entity<Screening>().HasData(
                new Screening { Id = 1, MovieId = 1, ScreenNumber = 1, Capacity = 100, StartTime = utc, CreatedAt = utc, UpdatedAt = utc },
                new Screening { Id = 2, MovieId = 2, ScreenNumber = 2, Capacity = 100, StartTime = utc, CreatedAt = utc, UpdatedAt = utc },
                new Screening { Id = 3, MovieId = 1, ScreenNumber = 3, Capacity = 40, StartTime = utc, CreatedAt = utc, UpdatedAt = utc }
            );

            // SEED Tickets
            modelBuilder.Entity<Ticket>().HasData(
                new Ticket { Id = 1, SeatNumber = 1, CustomerId = 1, ScreeningId = 1, CreatedAt = utc, UpdatedAt = utc },
                new Ticket { Id = 2, SeatNumber = 2, CustomerId = 2, ScreeningId = 2, CreatedAt = utc, UpdatedAt = utc },
                new Ticket { Id = 3, SeatNumber = 3, CustomerId = 1, ScreeningId = 3, CreatedAt = utc, UpdatedAt = utc }
            );
            

            // SEED Users with roles
            var usersAccounts = modelBuilder.Entity<ApplicationUser>();
            var hasher = new PasswordHasher<ApplicationUser>();
            
            // Seed Admin
            var adminUser = new ApplicationUser
            {
                Id = "admin-id", 
                UserName = "admin@example.com",
                NormalizedUserName = "ADMIN@EXAMPLE.COM",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                Role = Roles.Admin // Assigning the admin role
            };
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "adminpassword");
            usersAccounts.HasData(adminUser);

            // Seed Manager
            var managerUser = new ApplicationUser
            {
                Id = "manager-id", 
                UserName = "manager@example.com",
                NormalizedUserName = "MANAGER@EXAMPLE.COM",
                NormalizedEmail = "MANAGER@EXAMPLE.COM",
                Email = "manager@example.com",
                Role = Roles.Manager // Assigning the manager role
            };
            managerUser.PasswordHash = hasher.HashPassword(managerUser, "managerpassword");
            usersAccounts.HasData(managerUser);
            }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Screening> Screenings { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        
    }
}
