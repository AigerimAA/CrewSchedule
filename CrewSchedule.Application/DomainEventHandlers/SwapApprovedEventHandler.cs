using CrewSchedule.Application.Interfaces;
using CrewSchedule.Domain.Entity;
using CrewSchedule.Domain.Events;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

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
            //_context.Assignments = DbSet<Assignment> (обращение к таблице Assignments)
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

            fromAssignment = new Assignment(
                notification.FlightId,
                notification.ToCrewMemberId);

            toAssignment = new Assignment(
                notification.FlightId,
                notification.FromCrewMemberId);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
