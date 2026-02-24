using CrewSchedule.Application.Interfaces;
using CrewSchedule.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Application.Commands
{
    public record class CheckOutCommand(Guid FlightId, Guid CrewMemberId) : IRequest;

    public class CheckOutCommandHandler : IRequestHandler<CheckOutCommand>
    {
        private readonly IAssignmentRepository _repository;
        private readonly ILogger<CheckOutCommandHandler> _logger;

        public CheckOutCommandHandler(IAssignmentRepository repository, ILogger<CheckOutCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task Handle(CheckOutCommand request, CancellationToken cancellationToken)
        {
            var assignment = await _repository.GetAsync(request.FlightId, request.CrewMemberId, cancellationToken);

            if (assignment is null)
                throw new Exception("Assignment not found");

            assignment.CheckOut(DateTime.UtcNow);

            await _repository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Crew member {CrewMemberId} checked out from flight {FlightId}", request.CrewMemberId, request.FlightId);
        }
    }
}
