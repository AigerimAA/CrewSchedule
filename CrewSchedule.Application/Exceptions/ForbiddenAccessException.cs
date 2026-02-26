using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Application.Exceptions
{
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException() : base("Access denied.") { }
    }
}
