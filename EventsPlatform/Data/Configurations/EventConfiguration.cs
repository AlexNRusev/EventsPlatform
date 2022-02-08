using EventsPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsPlatform.Data.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasMany(e => e.Organizers)
                .WithMany(u => u.CreatedEvents);
            builder.HasMany(e => e.RegisteredParticipants)
                .WithMany(u => u.AppliedEvents);
        }
    }
}
