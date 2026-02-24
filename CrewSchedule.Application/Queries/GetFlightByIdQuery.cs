using CrewSchedule.Application.DTO;
using CrewSchedule.Application.Interfaces;
using CrewSchedule.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Application.Queries
{
    public record class GetFlightByIdQuery(Guid Id) : IRequest<FlightDto?>;
    
    public class GetFlightByIdQueryHandler : IRequestHandler<GetFlightByIdQuery, FlightDto?>
    {
        private readonly IFlightReadRepository _repository;

        public GetFlightByIdQueryHandler(IFlightReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<FlightDto?> Handle(GetFlightByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id, cancellationToken);
        }
    }
  
}
