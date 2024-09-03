namespace AccessControl.Domain.Interfaces.Permission
{
    public interface IPermissionUOW : IDisposable
    {
        IPermissionRepository Permissions { get; }
        IPermissionRepository PermissionElastic { get; } 
        IPermissionRepository PermissionKafka { get; }
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
