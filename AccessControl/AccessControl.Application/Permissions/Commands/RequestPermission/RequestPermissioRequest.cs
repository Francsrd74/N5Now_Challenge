using AccessControl.Domain.Entities;
using AccessControl.Domain.Events.Permission;
using AccessControl.Domain.Interfaces.Permission;
using Confluent.Kafka;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Application.Permissions.Commands.RequestPermission
{
    public class RequestPermissioRequest : IRequest<int>
    {
        public string EmployeeForename { get; set; } = null!;
        public string EmployeeSurname { get; set; } = null!;
        public int PermissionTypeId { get; set; }
    }

    public class RequestPermissioRequestHandler : IRequestHandler<RequestPermissioRequest, int>
    {
        private readonly IPermissionEntityFrameworkRepository _permissionEntityFrameworkRepository;
        private readonly ILogger<RequestPermissioRequestHandler> _logger;
        public RequestPermissioRequestHandler(IPermissionEntityFrameworkRepository permissionEntityFrameworkRepository, ILogger<RequestPermissioRequestHandler> logger)
        {
            this._permissionEntityFrameworkRepository = permissionEntityFrameworkRepository;
            _logger = logger;
        }
        async Task<int> IRequestHandler<RequestPermissioRequest, int>.Handle(RequestPermissioRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Init RequestPermissioRequest");

            var newPermission = new Permission()
            {
                EmployeeForename = request.EmployeeForename,
                EmployeeSurname = request.EmployeeSurname,
                PermissionTypeId = request.PermissionTypeId,
                PermissionDate = DateTime.Now
            };

            ///add DomainEvent Create Permission 
            newPermission.DomainEvents.Add(new PermissionCreateEvent(newPermission));


            newPermission = await _permissionEntityFrameworkRepository.AddAsync(newPermission, cancellationToken);
            if (newPermission == null)
                throw new Exception("No Entity Added");


            _logger.LogInformation("Complete RequestPermissioRequest");

            return newPermission.Id;
             

        }
    }
}
