using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Domain.Entity
{
    public class CrewMember
    {
        public Guid Id { get; private set; }
        public string FullName { get; private set; }
        public string Position { get; private set; }

        private CrewMember() { }
        public CrewMember(Guid id, string fullName, string position)
        {
            Id = id;
            FullName = fullName;
            Position = position;
        }
    }
}
