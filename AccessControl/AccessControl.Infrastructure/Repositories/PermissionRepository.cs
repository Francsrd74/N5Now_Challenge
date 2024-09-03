using AccessControl.Application.Common.Interfaces;
using AccessControl.Domain.Entities;
using AccessControl.Domain.Interfaces.Permission;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Infrastructure.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly IAccessControlContext _accessControlContext;

        public PermissionRepository(IAccessControlContext accessControlContext)
        {
            _accessControlContext = accessControlContext;
        }

        public Permission Get(int id)
        {
            return _accessControlContext.Permissions.AsNoTracking().FirstOrDefault(p => p.Id.Equals(id));
        }

        public Permission Add(Permission entity)
        {
            return _accessControlContext.Permissions.Add(entity).Entity; 
        }

        public void Update(Permission entity)
        {
            _accessControlContext.Permissions.Update(entity);
        }
    }
}
