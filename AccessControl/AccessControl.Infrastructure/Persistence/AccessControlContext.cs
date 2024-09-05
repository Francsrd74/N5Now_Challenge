using System;
using System.Collections.Generic;
using System.Reflection;
using AccessControl.Application.Common.Interfaces;
using AccessControl.Domain.Common;
using AccessControl.Domain.Entities;
using AccessControl.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AccessControl.Infrastructure.Persistence
{
    public partial class AccessControlContext : DbContext , IAccessControlContext
    {
        private readonly IDomainEventService _domainEventService;

        public AccessControlContext()
        {
        }

        public AccessControlContext(DbContextOptions<AccessControlContext> options, IDomainEventService domainEventService)
            : base(options)
        {
            _domainEventService = domainEventService;
        }

        public virtual DbSet<Permission> Permissions { get; set; } = null!;
        public virtual DbSet<PermissionType> PermissionTypes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

            await this.DispatchEvents();

            return result;
        }

        private async Task DispatchEvents()
        {
            while (true)
            {
                var domainEventEntity = ChangeTracker.Entries<IHasDomainEvent>()
                    .Select(x => x.Entity.DomainEvents)
                    .SelectMany(x => x)
                    .Where(domainEvent => !domainEvent.IsPublished)
                    .FirstOrDefault();
                if (domainEventEntity == null) break;

                domainEventEntity.IsPublished = true;
                await _domainEventService.Publish(domainEventEntity);
            }
        }


    }
}
