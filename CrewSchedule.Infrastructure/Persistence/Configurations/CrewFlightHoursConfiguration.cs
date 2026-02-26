using CrewSchedule.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Infrastructure.Persistence.Configurations
{
    public class CrewFlightHoursConfiguration : IEntityTypeConfiguration<CrewFlightHours>
    {
        public void Configure(EntityTypeBuilder<CrewFlightHours> builder)
        {
            builder.HasKey(x => x.CrewMemberId);

            builder.OwnsMany(x => x.FlightTimeEntries, entry =>
            {
                entry.WithOwner().HasForeignKey("CrewFlightHoursId");
                entry.Property(e => e.FlightDate).IsRequired();
                entry.Property(e => e.Minutes).IsRequired();
            });
        }
    }
}
