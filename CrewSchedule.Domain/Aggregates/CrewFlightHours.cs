using CrewSchedule.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Domain.Aggregates
{
    public class CrewFlightHours : BaseEntity
    {
        public Guid CrewMemberId { get; private set; }
        public int TotalMinutes { get; private set; }

        private CrewFlightHours() { }
        public CrewFlightHours(Guid crewMemberId)
        {
            CrewMemberId = crewMemberId;
            TotalMinutes = 0;
        }

        public void AddMinutes(int minutes)
        {
            if (minutes <= 0)
                throw new InvalidOperationException("Invalid minutes");

            if ((TotalMinutes + minutes) > 900 * 60)
                throw new InvalidOperationException("Annual limit exceeded.");

            TotalMinutes += minutes;
        }

        public void RemoveMinutes(int minutes)
        {
            if (minutes <= 0)
                throw new InvalidOperationException("Invalid minutes");

            if (TotalMinutes < minutes)
                throw new InvalidOperationException("Negative hours");

            TotalMinutes -= minutes;
        }
    }
}
