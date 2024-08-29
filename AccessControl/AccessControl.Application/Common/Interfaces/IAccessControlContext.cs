using AccessControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Application.Common.Interfaces
{
    public interface IAccessControlContext
    {
        DbSet<Permission> Permissions { get; set; }
        DbSet<PermissionType> PermissionTypes { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
