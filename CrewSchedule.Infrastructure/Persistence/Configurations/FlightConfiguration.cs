using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CrewSchedule.Domain.Entities;

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

            builder.HasMany<Assignment>()
                .WithOne()
                .HasForeignKey("FlightId")
                .IsRequired();

            builder.Navigation(x => x.Assignments)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasField("_assignments");

#if DEBUG
            var flightId = new Guid("11111111-1111-1111-1111-111111111111");
            var departure = new DateTime(2025, 3, 1, 10, 0, 0, DateTimeKind.Utc);
            var arrival = departure.AddHours(2);

            builder.HasData(
                new Flight(
                flightId,
                "KC123",
                "ALA",
                "SCO",
                departure,
                arrival));
#endif
        }
    }
}
