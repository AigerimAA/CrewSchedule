using CrewSchedule.Domain.Common;
using CrewSchedule.Domain.Enums;
using CrewSchedule.Domain.Events;
using CrewSchedule.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Domain.Aggregates
{
    public class SwapRequest : BaseEntity
    {
        public Guid Id { get; private set; }
        public Guid FromCrewMemberId { get; private set; }
        public Guid ToCrewMemberId { get; private set; }
        public Guid FlightId { get; private set; }
        public SwapStatus Status { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }

        private SwapRequest() { }

        public SwapRequest(Guid fromCrewMemberId, Guid toCrewMemberId, Guid flightId)
        {
            if (fromCrewMemberId == Guid.Empty)
                throw new DomainException("'From crew member Id' cannot be empty");

            if (toCrewMemberId == Guid.Empty)
                throw new DomainException("'To crew member Id' cannot be empty");

            if (flightId == Guid.Empty)
                throw new DomainException("Flight Id cannot be empty");

            if (fromCrewMemberId == toCrewMemberId)
                throw new DomainException("Cannot swap with yourself");

            Id = Guid.NewGuid();
            FromCrewMemberId = fromCrewMemberId;
            ToCrewMemberId = toCrewMemberId;
            FlightId = flightId;
            Status = SwapStatus.Pending;
            CreatedAtUtc = DateTime.UtcNow;
        }
        public void Approve(Guid approverId)
        {
            if (Status != SwapStatus.Pending)
                throw new InvalidOperationException("Swap already processed");
            
            if (approverId != ToCrewMemberId)
                throw new InvalidOperationException("Only target crew member can approve");

            Status = SwapStatus.Approved;

            AddDomainEvent(new SwapApprovedEvent(Id, FromCrewMemberId, ToCrewMemberId, FlightId));
        }

        public void Reject(Guid approverId)
        {
            if (Status != SwapStatus.Pending)
                throw new InvalidOperationException("Swap already processed");

            if (approverId != ToCrewMemberId)
                throw new InvalidOperationException("Only target crew member can reject");

            Status = SwapStatus.Rejected;
        }

        public void Cancel(Guid requesterId)
        {
            if (Status != SwapStatus.Pending)
                throw new InvalidOperationException("Swap already processed");

            if (requesterId != FromCrewMemberId)
                throw new InvalidOperationException("Only requester can cancel");

            Status = SwapStatus.Cancelled;

        }
    }
}
