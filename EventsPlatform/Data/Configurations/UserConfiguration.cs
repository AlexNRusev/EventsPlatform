using EventsPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsPlatform.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasMany(u => u.CreatedEvents)
                .WithMany(e => e.Organizers);
            builder.HasMany(u => u.AppliedEvents)
                .WithMany(e => e.RegisteredParticipants);
        }
    }
}
