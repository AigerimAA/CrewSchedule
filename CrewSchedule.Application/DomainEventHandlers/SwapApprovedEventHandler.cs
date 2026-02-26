using CrewSchedule.Application.Interfaces;
using CrewSchedule.Domain.Entities;
using CrewSchedule.Domain.Events;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using CrewSchedule.Application.Exceptions;

//Это реакция на событие (event handler в MediatR), а не обычный command handler
//Этот класс нужен, чтобы при одобрении свапа автоматически поменять местами экипаж в Assignments

namespace CrewSchedule.Application.DomainEventHandlers
{
    public class SwapApprovedEventHandler : INotificationHandler<SwapApprovedEvent>
    {
        private readonly ICrewDbContext _context;

        public SwapApprovedEventHandler(ICrewDbContext context)
        {
            _context = context;
        }

        public async Task Handle(SwapApprovedEvent notification, CancellationToken cancellationToken)
        {
            var fromAssignment = await _context.Assignments
                .FirstOrDefaultAsync(x =>
                    x.FlightId == notification.FlightId &&
                    x.CrewMemberId == notification.FromCrewMemberId,
                    cancellationToken);

            var toAssignment = await _context.Assignments
                .FirstOrDefaultAsync(x =>
                    x.FlightId == notification.FlightId &&
                    x.CrewMemberId == notification.ToCrewMemberId,
                    cancellationToken);

            if (fromAssignment is null)
                throw new NotFoundException("Assignment", $"flight {notification.FlightId}, crew {notification.FromCrewMemberId}");

            if (toAssignment is null)
                throw new NotFoundException("Assignment", $"flight {notification.FlightId}, crew {notification.ToCrewMemberId}");

            fromAssignment.Reassign(notification.ToCrewMemberId);
            toAssignment.Reassign(notification.FromCrewMemberId);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
