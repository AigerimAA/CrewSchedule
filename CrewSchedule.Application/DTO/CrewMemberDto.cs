using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Application.DTO
{
    public class CrewMemberDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
    }
}
