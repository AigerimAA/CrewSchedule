using CrewSchedule.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Infrastructure.Persistence.Configurations
{
    public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FlightId)
                .IsRequired();

            builder.Property(x => x.CrewMemberId)
                .IsRequired();

            builder.Property(x => x.CheckInTimeUtc);
            builder.Property(x => x.CheckOutTimeUtc);

            builder.HasOne<Flight>()
                .WithMany()
                .HasForeignKey(x => x.FlightId);

            builder.HasOne<CrewMember>()
                .WithMany()
                .HasForeignKey(x => x.CrewMemberId);

            builder.HasIndex(x => new { x.FlightId, x.CrewMemberId }).IsUnique();
        }
    }
}
