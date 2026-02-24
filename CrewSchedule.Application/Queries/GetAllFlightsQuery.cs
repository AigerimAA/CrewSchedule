using System;
using System.Collections.Generic;
using System.Text;
using CrewSchedule.Application.DTO;
using CrewSchedule.Application.Interfaces;
using CrewSchedule.Application.Repositories;
using MediatR;

namespace CrewSchedule.Application.Queries
{
    public record class GetAllFlightsQuery() : IRequest<List<FlightDto>>;
    
    public class GetAllFlightsQueryHandler : IRequestHandler<GetAllFlightsQuery, List<FlightDto>>
    {
        private readonly IFlightReadRepository _repository;
        public GetAllFlightsQueryHandler(IFlightReadRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<FlightDto>> Handle (GetAllFlightsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }
    }
}
