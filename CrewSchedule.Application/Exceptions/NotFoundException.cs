using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            :base($"{name} with id '{key}' was not found.")
        {
        }
    }
}
