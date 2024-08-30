using AccessControl.Domain.Entities;
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
        public DateTime PermissionDate { get; set; }
    }

    public class RequestPermissioRequestHandler : IRequestHandler<RequestPermissioRequest, int>
    {  
        Task<int> IRequestHandler<RequestPermissioRequest, int>.Handle(RequestPermissioRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
