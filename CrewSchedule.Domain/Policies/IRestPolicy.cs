using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Domain.Policies
{
    public interface IRestPolicy
    {
        void ValidateRest(DateTime previousDutyEndUtc, DateTime nextDutyStartUtc);
    }
}
