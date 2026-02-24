using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Domain.Events
{
    public sealed class SwapApprovedEvent : IDomainEvent
    {
        public Guid SwapId { get; }
        public Guid FromCrewMemberId { get; }
        public Guid ToCrewMemberId { get; }
        public Guid FlightId { get; }

        public SwapApprovedEvent(Guid swapId, Guid fromCrewMemberId, Guid toCrewMemberId, Guid flightId)
        {
            SwapId = swapId;
            FromCrewMemberId = fromCrewMemberId;
            ToCrewMemberId = toCrewMemberId;
            FlightId = flightId;
        }
    }
}
