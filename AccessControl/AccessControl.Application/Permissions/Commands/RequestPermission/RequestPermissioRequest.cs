using AccessControl.Domain.Entities;
using AccessControl.Domain.Interfaces.Permission;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IPermissionUOW permissionUOW;
        public RequestPermissioRequestHandler(IPermissionUOW permissionUOW)
        {
            this.permissionUOW = permissionUOW;
        }
        async Task<int> IRequestHandler<RequestPermissioRequest, int>.Handle(RequestPermissioRequest request, CancellationToken cancellationToken)
        {

            var newPermission = new Permission()
            {
                EmployeeForename = request.EmployeeForename,
                EmployeeSurname = request.EmployeeSurname,
                PermissionTypeId = request.PermissionTypeId,
                PermissionDate = DateTime.Now
            };

            var permisionNew = permissionUOW.Permissions.Add(newPermission);
            if (!await permissionUOW.SaveChangesAsync(cancellationToken))
                throw new Exception("No Entity Added");
             
            var permissionElasticAdded = permissionUOW.PermissionElastic.Add(newPermission);
            if (permissionElasticAdded == null)
                throw new Exception("No Entity Added to ElasticSearch");

            var permissionKafkaAdded = permissionUOW.PermissionKafka.Add(newPermission);
            if (permissionElasticAdded == null)
                throw new Exception("No Entity Added to Kafka");

            return permisionNew.Id;
        }
    }
}
