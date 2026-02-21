using Domain.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        private readonly IPublisher _publisher;

        public AppDbContext(DbContextOptions<AppDbContext> options, IPublisher publisher) : base(options)
        {
            _publisher = publisher;
        }

        public DbSet<Payment> Payments { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            await DispatchDomainEventsAsync();


            return result;
        }
        private async Task DispatchDomainEventsAsync()
        {
            var domainEntities = ChangeTracker
                .Entries<BaseEntity<dynamic>>() // Adjust generic if needed or use interface
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            // In reality, you might need a common non-generic base or interface to grab all entities
            // Let's assume BaseEntity has the events. 
            // Note: C# generics make "BaseEntity<dynamic>" tricky. 
            // Better approach: Make IDomainEventsContainer interface.

            // Simplified Logic for this example:
            var entitiesWithEvents = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity<Guid> || e.Entity is BaseEntity<int>)
                .Select(e => e.Entity)
                .ToList();

            foreach (var entity in entitiesWithEvents)
            {
                // Reflection or Interface casting to get events
                // In a real app, use a non-generic interface IHasDomainEvents
                // _publisher.Publish(event);
            }
        }
    }
}
