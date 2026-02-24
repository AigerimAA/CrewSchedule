using CrewSchedule.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace CrewSchedule.Application.IntegrationEvents
{
    public class SwapApprovedIntegrationEventHandler
    {
        private readonly IFlightHoursDbContext _context;

        public SwapApprovedIntegrationEventHandler(IFlightHoursDbContext context)
        {
            _context = context;
        }

        public async Task Handle(SwapApprovedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var from = await _context.CrewFlightHours.FirstAsync(x => x.CrewMemberId == notification.FromCrewMemberId, cancellationToken);

            var to = await _context.CrewFlightHours.FirstAsync(x => x.CrewMemberId == notification.ToCrewMemberId, cancellationToken);

            from.RemoveMinutes(notification.FlightDurationMinutes);
            to.AddMinutes(notification.FlightDurationMinutes);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
