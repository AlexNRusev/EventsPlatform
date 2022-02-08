using EventsPlatform.Data.Configurations;
using EventsPlatform.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventsPlatform.Data
{
    public class EventsPlatformDbContext : IdentityDbContext<User>
    {
        public EventsPlatformDbContext(DbContextOptions options) :base(options) { }

        public DbSet<Event> Events { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new EventConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new LocationConfiguration());

            builder.Entity<IdentityRole>().HasData(
                new List<IdentityRole>()
                {
                    new IdentityRole()
                    {
                        Id = "515de595-b492-44e5-ab56-dfdbcc6626b2",
                        Name = "User",
                        NormalizedName = "User".ToUpper()
                    },
                    new IdentityRole()
                    {
                        Id = "7a5148fc-e447-4ca8-8e35-bc4b468c93fd",
                        Name = "Administrator",
                        NormalizedName = "Administrator".ToUpper()
                    }
                });

            var hasher = new PasswordHasher<IdentityUser>();
            builder.Entity<User>().HasData(
                new List<User>()
                {
                    new User()
                    {
                        Id = "7a5148fc-e447-4ca8-8e35-bc4b468c93fe",
                        UserName = "alex",
                        Email = "alrusev@tu-sofia.bg",
                        FirstName = "Alexander",
                        LastName = "Rusev",
                        IsBanned = false,
                        NormalizedUserName = "ALEX",
                        PasswordHash = hasher.HashPassword(null, "alex"),
                        CreatedEvents = new List<Event>(),
                        AppliedEvents = new List<Event>()
                    }
                });

            builder.Entity<IdentityUserRole<string>>().HasData(
                new List<IdentityUserRole<string>>()
                {
                    new IdentityUserRole<string>()
                    {
                        RoleId = "7a5148fc-e447-4ca8-8e35-bc4b468c93fd",
                        UserId = "7a5148fc-e447-4ca8-8e35-bc4b468c93fe"
                    }
                });
        }

    }
}
