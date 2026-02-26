using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Infrastructure.Persistence
{
    public class CrewDbContextFactory : IDesignTimeDbContextFactory<CrewDbContext>
    {
        public CrewDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CrewDbContext>();

            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=CrewScheduleDb;Trusted_Connection=True;";

            optionsBuilder.UseSqlServer(connectionString);

            return new CrewDbContext(optionsBuilder.Options, mediator: null);
        }
    }
}
