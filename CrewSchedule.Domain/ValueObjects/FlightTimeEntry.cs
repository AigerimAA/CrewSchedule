using CrewSchedule.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Domain.ValueObjects
{
    public class FlightTimeEntry
    {
        public DateTime FlightDate { get; }
        public int Minutes { get; }

        public FlightTimeEntry(DateTime flightDate, int minutes)
        {
            if (flightDate == default)
                throw new DomainException("Flight date cannot be empty");

            if (minutes <= 0)
                throw new DomainException("Flight duration must be greater than zero");

            FlightDate = flightDate;
            Minutes = minutes;
        }

        public bool IsValid() => Minutes > 0 && FlightDate != default;
    }
}
