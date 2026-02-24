using CrewSchedule.Domain.Aggregates;
using CrewSchedule.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Application.Interfaces
{
    public interface ICrewDbContext
    {
        DbSet<Flight> Flights { get; }
        DbSet<CrewMember> CrewMembers { get; }
        DbSet<SwapRequest> SwapRequests { get; }
        DbSet<Assignment> Assignments { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
