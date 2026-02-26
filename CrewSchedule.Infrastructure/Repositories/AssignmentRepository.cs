using CrewSchedule.Application.Interfaces;
using CrewSchedule.Application.Repositories;
using CrewSchedule.Domain.Entities;
using CrewSchedule.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Infrastructure.Repositories
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly CrewDbContext _context;

        public AssignmentRepository(CrewDbContext context)
        {
            _context = context;
        }

        public async Task<Assignment?> GetAsync(Guid flightId, Guid crewMemberId, CancellationToken cancellationToken)
        {
            return await _context.Assignments.FirstOrDefaultAsync(x => x.FlightId == flightId && x.CrewMemberId == crewMemberId, cancellationToken);

        }
        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
