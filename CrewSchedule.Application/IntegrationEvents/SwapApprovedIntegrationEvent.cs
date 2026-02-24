using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Application.IntegrationEvents
{
    public class SwapApprovedIntegrationEvent
    {
        public Guid FromCrewMemberId { get; }
        public Guid ToCrewMemberId { get; }
        public Guid FlightId { get; }
        public int FlightDurationMinutes { get; }

        public SwapApprovedIntegrationEvent(Guid fromCrewMemberId, Guid toCrewMemberId, Guid flightId, int duration)
        {
            FromCrewMemberId = fromCrewMemberId;
            ToCrewMemberId = toCrewMemberId;
            FlightId = flightId;
            FlightDurationMinutes = duration;
        }
    }
}
