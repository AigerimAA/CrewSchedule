using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace CrewSchedule.Domain.Events
{
    public interface IDomainEvent : INotification
    {
    }
}
