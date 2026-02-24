using CrewSchedule.Application.Interfaces;
using CrewSchedule.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Application.Commands
{
    public record class CheckInCommand(Guid FlightId, Guid CrewMemberId) : IRequest;

    public class CheckInCommandHandler : IRequestHandler<CheckInCommand>
    {
        private readonly IAssignmentRepository _repository;
        private readonly ILogger<CheckInCommandHandler> _logger;

        public CheckInCommandHandler(IAssignmentRepository repository, ILogger<CheckInCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task Handle(CheckInCommand request, CancellationToken cancellationToken)
        {
            var assignment = await _repository.GetAsync(request.FlightId, request.CrewMemberId, cancellationToken);

            if (assignment is null)
                throw new Exception("Assignment not found");

            assignment.CheckIn(DateTime.UtcNow);

            await _repository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Crew member {CrewMemberId} checked in for flight {FlightId}", request.CrewMemberId, request.FlightId);

        }
    }
}
