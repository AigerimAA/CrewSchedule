using CrewSchedule.Application.Queries;
using CrewSchedule.Domain.Aggregates;
using CrewSchedule.Domain.Entities;

namespace CrewSchedule.Application.Interfaces
{
    public interface IAssignmentRepository
    {
        Task<Assignment?> GetAsync(Guid flightId, Guid crewMemberId, CancellationToken cancellationToken);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}