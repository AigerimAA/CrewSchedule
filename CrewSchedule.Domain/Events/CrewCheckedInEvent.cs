using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Domain.Events
{
    public class CrewCheckedInEvent : IDomainEvent
    {
        public Guid FlightId { get; }
        public Guid CrewMemberId { get; }
        public DateTime CheckedInAtUtc { get; }

        public CrewCheckedInEvent(Guid flightId, Guid crewMemberId, DateTime checkedInAtUtc)
        {
            FlightId = flightId;
            CrewMemberId = crewMemberId;
            CheckedInAtUtc = checkedInAtUtc;
        }
    }
}
