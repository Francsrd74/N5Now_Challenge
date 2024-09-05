using AccessControl.Domain.Entities;
using AccessControl.Domain.Events.Permission;
using AccessControl.Domain.Interfaces.Permission;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Application.Permissions.Commands.UpdatePermission
{
    public class UpdatePermissionRequest : IRequest<int>
    {
        public int Id { get; set; }
        public string EmployeeForename { get; set; } = null!;
        public string EmployeeSurname { get; set; } = null!;
        public int PermissionTypeId { get; set; }
    }

    public class UpdatePermissionRequesttHandler : IRequestHandler<UpdatePermissionRequest, int>
    {
        private readonly IPermissionEntityFrameworkRepository _permissionEntityFrameworkRepository;
        private readonly ILogger<UpdatePermissionRequesttHandler> _logger;
        public UpdatePermissionRequesttHandler(IPermissionEntityFrameworkRepository permissionEntityFrameworkRepository, ILogger<UpdatePermissionRequesttHandler> logger)
        {
            this._permissionEntityFrameworkRepository = permissionEntityFrameworkRepository;
            _logger = logger;
        }

        async Task<int> IRequestHandler<UpdatePermissionRequest, int>.Handle(UpdatePermissionRequest request, CancellationToken cancellationToken)
        {

            _logger.LogInformation("init UpdatePermissionRequesttHandler");

            var permission = await _permissionEntityFrameworkRepository.GetAsync(request.Id, cancellationToken);

            if (permission != null)
            {
                permission.EmployeeSurname = request.EmployeeSurname;
                permission.EmployeeForename = request.EmployeeForename;
                permission.PermissionTypeId = request.PermissionTypeId;     
                permission.PermissionDate = DateTime.Now;

                ///add DomainEvent Update Permission
                permission.DomainEvents.Add(new PermissionUpdateEvent(permission));


                await _permissionEntityFrameworkRepository.UpdateAsync(permission, cancellationToken); 
            }


            _logger.LogInformation("Complete UpdatePermissionRequesttHandler");

            return await Task.FromResult(permission.Id);
        }
    }
}
