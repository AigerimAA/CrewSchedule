using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Domain.ValueObjects
{
    public class FlightTimeEntry
    {
        public DateTime FlightDate { get; private set; }
        public int Minutes { get; private set; }

        public FlightTimeEntry(DateTime flightDate, int minutes)
        {
            FlightDate = flightDate;
            Minutes = minutes;
        }
    }
}
