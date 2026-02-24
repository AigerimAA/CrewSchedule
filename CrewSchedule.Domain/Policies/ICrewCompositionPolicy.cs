using CrewSchedule.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Domain.Policies
{
    public interface ICrewCompositionPolicy
    {
        void Validate(IReadOnlyCollection<CrewRole> crewRoles);
    }
}
