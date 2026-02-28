using System;
using System.Collections.Generic;
using System.Text;
using CrewSchedule.Application.Common;
using CrewSchedule.Application.DTO;
using CrewSchedule.Application.Interfaces;
using CrewSchedule.Application.Repositories;
using MediatR;

namespace CrewSchedule.Application.Queries
{
    public record class GetAllFlightsQuery(int Page = 1, int PageSize = 10) : IRequest<PaginatedList<FlightDto>>;
    
    public class GetAllFlightsQueryHandler : IRequestHandler<GetAllFlightsQuery, PaginatedList<FlightDto>>
    {
        private readonly IFlightReadRepository _repository;
        public GetAllFlightsQueryHandler(IFlightReadRepository repository)
        {
            _repository = repository;
        }
        public async Task<PaginatedList<FlightDto>> Handle (GetAllFlightsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(request.Page, request.PageSize, cancellationToken);
        }
    }
}
