using CrewSchedule.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Application.DomainEventHandlers
{
    public class CrewCheckedInEventHandler : INotificationHandler<CrewCheckedInEvent>
    {
        private readonly ILogger<CrewCheckedInEventHandler> _logger;

        public CrewCheckedInEventHandler(ILogger<CrewCheckedInEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(CrewCheckedInEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Event: Crew member {CrewMemberId} checked in for flight {FlightId} at {Time}",
                notification.CrewMemberId,
                notification.FlightId,
                notification.CheckedInAtUtc);

            return Task.CompletedTask;
        }
    }
}
