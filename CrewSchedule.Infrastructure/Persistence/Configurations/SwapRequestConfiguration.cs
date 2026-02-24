using CrewSchedule.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Infrastructure.Persistence.Configurations
{
    public class SwapRequestConfiguration : IEntityTypeConfiguration<SwapRequest>
    {
        public void Configure(EntityTypeBuilder<SwapRequest> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status)
                .HasConversion<int>();

            builder.Property(x => x.FromCrewMemberId)
                .IsRequired();

            builder.Property(x => x.ToCrewMemberId)
                .IsRequired();

            builder.Property(x => x.FlightId)
                .IsRequired();
        }
    }
}
