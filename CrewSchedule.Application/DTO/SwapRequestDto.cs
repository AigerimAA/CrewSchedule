using CrewSchedule.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Application.DTO
{
    public class SwapRequestDto
    {
        public Guid Id { get; set; }
        public Guid FromCrewMemberId { get; set; }
        public Guid ToCrewMemberId { get; set; }
        public Guid FlightId { get; set; }
        public SwapStatus Status { get; set; }
    }
}
