using CrewSchedule.Application.Interfaces;
using CrewSchedule.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Infrastructure.Persistence
{
    public class FlightHoursDbContext : DbContext, IFlightHoursDbContext
    {
        public FlightHoursDbContext(DbContextOptions<FlightHoursDbContext> options)
            :base(options)
        {
        }

        public DbSet<CrewFlightHours> CrewFlightHours => Set<CrewFlightHours>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FlightHoursDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
