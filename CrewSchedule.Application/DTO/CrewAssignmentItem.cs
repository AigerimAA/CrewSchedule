using CrewSchedule.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Application.DTO
{
    public record class CrewAssignmentItem(Guid CrewMemberId, CrewRole Role);
    
}
