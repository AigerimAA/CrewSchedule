using Microsoft.EntityFrameworkCore;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using CrewSchedule.Domain.Entity;
using CrewSchedule.Domain.Common;
using CrewSchedule.Domain.Events;
using CrewSchedule.Application.Interfaces;
using CrewSchedule.Domain.Aggregates;
using CrewSchedule.Application.Interfaces;

namespace CrewSchedule.Infrastructure.Persistence
{
    public class CrewDbContext : DbContext, ICrewDbContext
    {
        private readonly IMediator _mediator;
        public CrewDbContext(DbContextOptions<CrewDbContext> options, IMediator mediator)
            :base(options)
        {
            _mediator = mediator;
        }

        public DbSet<Flight> Flights => Set<Flight>();
        public DbSet<CrewMember> CrewMembers => Set<CrewMember>();
        public DbSet<Assignment> Assignments => Set<Assignment>();
        public DbSet<SwapRequest> SwapRequests => Set<SwapRequest>();

        public CrewDbContext(DbContextOptions<CrewDbContext> options) : base(options) { }

        public CrewDbContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CrewDbContext).Assembly);

            modelBuilder.Entity<Flight>(entity =>
            {
                entity.Navigation(e => e.Assignments)
                      .UsePropertyAccessMode(PropertyAccessMode.Field)
                      .HasField("_assignments");
            });

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var domainEvents = ChangeTracker
                .Entries<BaseEntity>()
                .Select(x => x.Entity)
                .Where(x => x.DomainEvents.Any())
                .SelectMany(x => x.DomainEvents)
                .ToList();

            var result = await base.SaveChangesAsync(cancellationToken);

            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent, cancellationToken);
            }

            foreach (var entity in ChangeTracker.Entries<BaseEntity>())
                entity.Entity.ClearDomainEvents();

            return result;
        }
    }
}
