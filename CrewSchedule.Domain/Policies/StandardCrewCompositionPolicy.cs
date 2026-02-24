using CrewSchedule.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Domain.Policies
{
    public class StandardCrewCompositionPolicy : ICrewCompositionPolicy
    {
        public void Validate(IReadOnlyCollection<CrewRole> roles)
        {
            if (!roles.Contains(CrewRole.Captain))
                throw new InvalidOperationException("Captain is required");

            if (!roles.Contains(CrewRole.IFS))
                throw new InvalidOperationException("IFS is required");

            var pilots = roles.Count(x =>
                x == CrewRole.Captain ||
                x == CrewRole.FirstOfficer ||
                x == CrewRole.FlightCrewInstructor);

            if (pilots < 2)
                throw new InvalidOperationException("Minimum 2 pilots required");

            var faCount = roles.Count(x =>
                x == CrewRole.IFS ||
                x == CrewRole.Purser ||
                x == CrewRole.BusinessFA ||
                x == CrewRole.EconomyFA);

            if (faCount < 4)
                throw new InvalidOperationException("Minimum 4 flight attendants required");
            
        }
    }
}
