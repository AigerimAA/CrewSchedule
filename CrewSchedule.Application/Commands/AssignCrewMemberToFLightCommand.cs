using CrewSchedule.Application.Interfaces;
using CrewSchedule.Domain.Entities;
using CrewSchedule.Domain.Policies;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Application.Commands
{
    public record class AssignCrewMemberToFlightCommand(Guid FlightId, Guid CrewMemberId, DateTime FlightStartUtc) : IRequest;

    public class AssignCrewMemberToFlightCommandHandler : IRequestHandler<AssignCrewMemberToFlightCommand>
    {
        private readonly ICrewDbContext _context;
        private readonly IRestPolicy _restPolicy;

        public AssignCrewMemberToFlightCommandHandler(ICrewDbContext context, IRestPolicy restPolicy)
        {
            _context = context;
            _restPolicy = restPolicy;
        }

        public async Task Handle(AssignCrewMemberToFlightCommand request, CancellationToken cancellationToken)
        {
            var lastAssignment = await _context.Assignments
                .Where(x => x.CrewMemberId == request.CrewMemberId)
                .OrderByDescending(x => x.CheckOutTimeUtc)
                .FirstOrDefaultAsync(cancellationToken);

            if (lastAssignment is not null && lastAssignment.CheckOutTimeUtc.HasValue)
            {
                _restPolicy.ValidateRest(lastAssignment.CheckOutTimeUtc.Value, request.FlightStartUtc);
            }

            var assignment = new Assignment(request.FlightId, request.CrewMemberId);

            _context.Assignments.Add(assignment);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
    
}
