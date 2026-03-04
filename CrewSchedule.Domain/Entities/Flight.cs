using CrewSchedule.Domain.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CrewSchedule.Domain.Entities
{
    public class Flight : BaseEntity
    {
        public Guid Id { get; private set; }
        public string FlightNumber { get; private set; }
        public string DepartureAirport { get; private set; }
        public string ArrivalAirport { get; private set; }
        public DateTime DepartureTimeUtc { get; private set; }
        public DateTime ArrivalTimeUtc { get; private set; }

        private readonly List<Assignment> _assignments = new();
        public IReadOnlyCollection<Assignment> Assignments => _assignments;

        private Flight() { }
        public Flight(Guid id, string flightNumber, string departureAirport, string arrivalAirport, DateTime departureTimeUtc, DateTime arrivalTimeUtc)
        {
            Id = id;
            FlightNumber = flightNumber;
            DepartureAirport = departureAirport;
            ArrivalAirport = arrivalAirport;
            DepartureTimeUtc = departureTimeUtc;
            ArrivalTimeUtc = arrivalTimeUtc;
        }

    }
}
