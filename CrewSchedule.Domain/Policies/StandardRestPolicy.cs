using System;
using System.Collections.Generic;
using System.Text;
using CrewSchedule.Domain.Policies;

namespace CrewSchedule.Domain.Policies
{
    public class StandardRestPolicy : IRestPolicy
    {
        private const int MinimumRestHours = 12;

        public void ValidateRest(DateTime previousDutyEndUtc, DateTime nextDutyStartUtc)
        {
            var rest = nextDutyStartUtc - previousDutyEndUtc;

            if (rest.TotalHours < MinimumRestHours)
                throw new InvalidOperationException($"Minimum rest is {MinimumRestHours} hours.");
        }
    }
}
