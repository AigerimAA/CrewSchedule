using CrewSchedule.Domain.Common;
using CrewSchedule.Domain.Events;
using CrewSchedule.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Domain.Entities
{
    public class Assignment : BaseEntity
    {
        public Guid Id { get; private set; }
        public Guid FlightId { get; private set; }
        public Guid CrewMemberId { get; private set; }

        public DateTime? CheckInTimeUtc { get; private set; }
        public DateTime? CheckOutTimeUtc { get; private set; }

        private Assignment() { }
        public Assignment(Guid flightId, Guid crewMemberId)
        {
            if (flightId == Guid.Empty)
                throw new DomainException("Flight Id cannot be empty");

            if (crewMemberId == Guid.Empty)
                throw new DomainException("CrewMemberId cannot be empty");

            Id = Guid.NewGuid();
            FlightId = flightId;
            CrewMemberId = crewMemberId;
        }

        public void Reassign(Guid newCrewMemberId)
        {
            if (newCrewMemberId == Guid.Empty)
                throw new DomainException("New crew member Id cannot be empty");

            if (CrewMemberId == newCrewMemberId)
                throw new DomainException("Already assigned");

            if (CheckInTimeUtc.HasValue)
                throw new DomainException("Cannot reassign after check-in");

            CrewMemberId = newCrewMemberId;
        }

        public void CheckIn(DateTime checkInTimeUtc)
        {
            if (CheckInTimeUtc.HasValue)
                throw new DomainException("Crew member already checked in");

            CheckInTimeUtc = checkInTimeUtc;

            AddDomainEvent(new CrewCheckedInEvent(FlightId, CrewMemberId, checkInTimeUtc));
        }

        public void CheckOut(DateTime checkoutTimeUtc)
        {
            if (!CheckInTimeUtc.HasValue)
                throw new DomainException("Cannot check out before check in");

            if (CheckOutTimeUtc.HasValue)
                throw new DomainException("Already checked out");

            if (checkoutTimeUtc < CheckInTimeUtc.Value)
                throw new DomainException("Checkout time invalid");
            
            CheckOutTimeUtc = checkoutTimeUtc;
        }

    }
}
