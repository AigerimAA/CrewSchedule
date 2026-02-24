using CrewSchedule.Application.DTO;
using CrewSchedule.Application.Interfaces;
using CrewSchedule.Domain.Entity;
using CrewSchedule.Domain.Policies;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Application.Commands
{
    public record class AssignFullCrewToFlightCommand(Guid FlightId, DateTime FlightStartUtc, DateTime FlightEndUtc, List<CrewAssignmentItem> Crew) : IRequest;

    public class AssignFullCrewToFlightCommandHandler : IRequestHandler<AssignFullCrewToFlightCommand>
    {
        private readonly ICrewDbContext _context;
        private readonly IRestPolicy _restPolicy;
        private readonly ICrewCompositionPolicy _compositionPolicy;
        private readonly ILogger<AssignFullCrewToFlightCommandHandler> _logger;

        public AssignFullCrewToFlightCommandHandler(ICrewDbContext context, IRestPolicy restPolicy, ICrewCompositionPolicy compositionPolicy, ILogger<AssignFullCrewToFlightCommandHandler> logger)
        {
            _context = context;
            _restPolicy = restPolicy;
            _compositionPolicy = compositionPolicy;
            _logger = logger;
        }
        
        public async Task Handle(AssignFullCrewToFlightCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Assigning crew to flight {FlightId}", request.FlightId);

            var roles = request.Crew.Select(x => x.Role).ToList();

            _compositionPolicy.Validate(roles);

            foreach (var crewMember in request.Crew)
            {
                var lastAssignment = await _context.Assignments
                    .Where(x => x.CrewMemberId == crewMember.CrewMemberId)
                    .OrderByDescending(x => x.CheckOutTimeUtc)
                    .FirstOrDefaultAsync(cancellationToken);

                if (lastAssignment?.CheckOutTimeUtc != null)
                {
                    _restPolicy.ValidateRest(lastAssignment.CheckOutTimeUtc.Value,
                        request.FlightStartUtc);
                }

                var assignment = new Assignment(request.FlightId, crewMember.CrewMemberId);

                _context.Assignments.Add(assignment);

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Crew assigned successfully to flight {FlightId}", request.FlightId);
            }
        }
    }
    
}
