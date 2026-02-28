using CrewSchedule.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(CrewMember crewMember);
    }
}
