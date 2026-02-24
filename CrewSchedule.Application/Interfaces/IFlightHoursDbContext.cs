using CrewSchedule.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Application.Interfaces
{
    public interface IFlightHoursDbContext
    {
        DbSet<CrewFlightHours> CrewFlightHours { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
