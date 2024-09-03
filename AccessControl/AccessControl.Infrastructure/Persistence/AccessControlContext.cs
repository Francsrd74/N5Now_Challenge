using System;
using System.Collections.Generic;
using System.Reflection;
using AccessControl.Application.Common.Interfaces;
using AccessControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AccessControl.Infrastructure.Persistence
{
    public partial class AccessControlContext : DbContext , IAccessControlContext
    {
        public AccessControlContext()
        {
        }

        public AccessControlContext(DbContextOptions<AccessControlContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Permission> Permissions { get; set; } = null!;
        public virtual DbSet<PermissionType> PermissionTypes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }


    }
}
