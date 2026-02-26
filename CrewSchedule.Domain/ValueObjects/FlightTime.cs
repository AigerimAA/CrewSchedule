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
            if (arrivalUtc <= departureUtc)
                throw new ArgumentException($"Arrival time ({arrivalUtc}) must be later than departure time ({departureUtc})");

            Duration = arrivalUtc - departureUtc;
        }
    }
}
