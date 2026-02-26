using CrewSchedule.Domain.Common;
using CrewSchedule.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CrewSchedule.Domain.Aggregates
{
    public class CrewFlightHours : BaseEntity
    {
        public Guid CrewMemberId { get; private set; }

        private readonly List<FlightTimeEntry> _flightTimeEntries = new();
        public IReadOnlyCollection<FlightTimeEntry> FlightTimeEntries => _flightTimeEntries.AsReadOnly();
        
        //Годовой налет члена экипажа не должен превышать 900 часов в год (скользящий год)
        private const int MaxAnnualFlightMinutes = 900 * 60;

        private CrewFlightHours() { }
        public CrewFlightHours(Guid crewMemberId)
        {
            CrewMemberId = crewMemberId;
        }

        public void AddMinutes(int minutes, DateTime flightDate)
        {
            if (minutes <= 0)
                throw new InvalidOperationException("Invalid minutes");

            var oneYearAgo = flightDate.AddYears(-1);
            _flightTimeEntries.RemoveAll(e => e.FlightDate < oneYearAgo);

            var currentTotal = _flightTimeEntries.Sum(e => e.Minutes);

            if (currentTotal + minutes > MaxAnnualFlightMinutes)
                throw new InvalidOperationException($"Annual limit of {MaxAnnualFlightMinutes / 60} hours exceeded for the last 12 months");

            _flightTimeEntries.Add(new FlightTimeEntry(flightDate, minutes));

        }
    }
}
