using CrewSchedule.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Application.Interfaces
{
    public interface ISwapReadRepository
    {
        Task<SwapRequestDto?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);

        Task<List<SwapRequestDto>> GetByCrewMemberAsync(Guid crewMemberId, CancellationToken cancellationToken);
    }
}
