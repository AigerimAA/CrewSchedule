using CrewSchedule.Domain.Common;
using CrewSchedule.Domain.Enums;
using CrewSchedule.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Domain.Entities
{
    public class CrewMember : BaseEntity
    {
        public Guid Id { get; private set; }
        public string FullName { get; private set; }
        public CrewRole Role { get; private set; }

        private CrewMember() { }
        public CrewMember(string fullName, CrewRole role)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Full name cannot be empty.", nameof(fullName));

            Id = Guid.NewGuid();
            FullName = fullName;
            Role = role;
        }

        public void CheckInForFlight(Guid flightId)
        {
            AddDomainEvent(new CrewCheckedInEvent(flightId, this.Id, DateTime.UtcNow));
        }
    }
}
