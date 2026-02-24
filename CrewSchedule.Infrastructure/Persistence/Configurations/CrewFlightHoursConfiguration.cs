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

            builder.Property(x => x.TotalMinutes).IsRequired();
        }
    }
}
