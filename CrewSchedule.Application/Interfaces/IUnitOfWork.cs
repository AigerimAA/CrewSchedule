using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
