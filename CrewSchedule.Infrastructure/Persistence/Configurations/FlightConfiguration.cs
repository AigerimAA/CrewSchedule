using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CrewSchedule.Domain.Entity;

namespace CrewSchedule.Infrastructure.Persistence.Configurations
{
    public class FlightConfiguration : IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FlightNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.DepartureAirport)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(x => x.ArrivalAirport)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(x => x.DepartureTimeUtc)
                .IsRequired();

            builder.Property(x => x.ArrivalTimeUtc)
                .IsRequired();

            builder.HasMany(typeof(Assignment), "_assignments")
                .WithOne()
                .HasForeignKey("FlightId");

        }
    }
}
