using CrewSchedule.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Application.Repositories
{
    public interface ISwapRequestRepository
    {
        Task<SwapRequest?> GetAsync(Guid id, CancellationToken cancellationToken);

        Task AddAsync(SwapRequest swap, CancellationToken cancellationToken);

    }
}
