using CrewSchedule.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Application.Interfaces
{
    public interface IFlightReadRepository
    {
        Task<FlightDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<CrewMemberDto>> GetCrewForFlightAsync(Guid flightId, CancellationToken cancellationToken);
        Task<List<FlightDto>> GetAllAsync(CancellationToken cancellationToken);
    }
}
