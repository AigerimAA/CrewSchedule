using CrewSchedule.Application.DTO;
using CrewSchedule.Application.Interfaces;
using CrewSchedule.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Application.Queries
{
    public record class GetCrewForFlightQuery(Guid FlightId) : IRequest<List<CrewMemberDto>>;

    public class GetCrewMemberForFlightQueryHandler : IRequestHandler<GetCrewForFlightQuery, List<CrewMemberDto>>
    {
        private readonly IFlightReadRepository _repository;

        public GetCrewMemberForFlightQueryHandler(IFlightReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CrewMemberDto>> Handle(GetCrewForFlightQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetCrewForFlightAsync(request.FlightId, cancellationToken);
        }
    }
    
}
