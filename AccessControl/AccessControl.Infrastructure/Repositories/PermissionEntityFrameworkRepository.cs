using AccessControl.Application.Common.Interfaces;
using AccessControl.Domain.Entities;
using AccessControl.Domain.Interfaces;
using AccessControl.Domain.Interfaces.Permission;
using Elastic.Clients.Elasticsearch;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Infrastructure.Repositories
{
    public class PermissionEntityFrameworkRepository : IPermissionEntityFrameworkRepository
    {
        private readonly IAccessControlContext _accessControlContext;

        public PermissionEntityFrameworkRepository(IAccessControlContext accessControlContext)
        {
            _accessControlContext = accessControlContext;
        }

        public async Task<Permission> GetAsync(int id, CancellationToken cancellationToken)
        {
            return _accessControlContext.Permissions.AsNoTracking().FirstOrDefault(p => p.Id.Equals(id));
        }

        public async Task<Permission> AddAsync(Permission entity, CancellationToken cancellationToken)
        {
            var result = await _accessControlContext.Permissions.AddAsync(entity);

            await _accessControlContext.SaveChangesAsync(cancellationToken);

            return result.Entity;
        }

        public async Task UpdateAsync(Permission entity, CancellationToken cancellationToken)
        {
            _accessControlContext.Permissions.Update(entity);
            await _accessControlContext.SaveChangesAsync(cancellationToken);

        }

    }
}
