using AccessControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Application.Common.Interfaces
{
    public interface IAccessControlContext
    {
        DbSet<Permission> Permissions { get; set; }
        DbSet<PermissionType> PermissionTypes { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
