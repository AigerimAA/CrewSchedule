using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Domain.ValueObjects
{
    public class FlightTime
    {
        public TimeSpan Duration { get; }
        public FlightTime(DateTime departureUtc, DateTime arrivalUtc)
        {
            Duration = arrivalUtc - departureUtc;
        }
    }
}
