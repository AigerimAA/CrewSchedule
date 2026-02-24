using CrewSchedule.Domain.Common;
using CrewSchedule.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Domain.Entity
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
            Id = Guid.NewGuid();
            FlightId = flightId;
            CrewMemberId = crewMemberId;
        }

        public void Reassign(Guid newCrewMemberId)
        {
            if (CrewMemberId == newCrewMemberId)
                throw new InvalidOperationException("Already assigned");
            if (CheckInTimeUtc.HasValue)
                throw new InvalidOperationException("Cannot reassign after check-in");

            CrewMemberId = newCrewMemberId;
        }

        public void CheckIn(DateTime checkInTimeUtc)
        {
            if (CheckInTimeUtc.HasValue)
                throw new InvalidOperationException("Crew member already checked in");

            CheckInTimeUtc = checkInTimeUtc;

            AddDomainEvent(new CrewCheckedInEvent(FlightId, CrewMemberId, checkInTimeUtc));
        }

        public void CheckOut(DateTime checkoutTimeUtc)
        {
            if (!CheckInTimeUtc.HasValue)
                throw new InvalidOperationException("Cannot check out before check in");

            if (CheckOutTimeUtc.HasValue)
                throw new InvalidOperationException("Already checked out");

            if (checkoutTimeUtc < CheckInTimeUtc.Value)
                throw new InvalidOperationException("Checkout time invallid");
            
            CheckOutTimeUtc = checkoutTimeUtc;
        }

    }
}
