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
        public DateTime FlightDate { get; set; }
        public int FlightDurationMinutes { get; }

        public SwapApprovedIntegrationEvent(Guid fromCrewMemberId, Guid toCrewMemberId, Guid flightId, int duration, DateTime flightDate)
        {
            FromCrewMemberId = fromCrewMemberId;
            ToCrewMemberId = toCrewMemberId;
            FlightId = flightId;
            FlightDurationMinutes = duration;
            FlightDate = flightDate;
        }
    }
}
