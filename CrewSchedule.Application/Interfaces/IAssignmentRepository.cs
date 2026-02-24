using CrewSchedule.Application.Queries;
using CrewSchedule.Domain.Aggregates;
using CrewSchedule.Domain.Entity;

namespace CrewSchedule.Application.Interfaces
{
    public interface IAssignmentRepository
    {
        Task<Assignment?> GetAsync(Guid flightId, Guid CrewMemberId, CancellationToken cancellationToken);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}