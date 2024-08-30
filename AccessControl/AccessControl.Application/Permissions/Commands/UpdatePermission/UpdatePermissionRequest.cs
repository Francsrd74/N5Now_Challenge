using AccessControl.Domain.Entities;
using MediatR;
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
        public DateTime PermissionDate { get; set; }
    }

    public class UpdatePermissionRequesttHandler : IRequestHandler<UpdatePermissionRequest, int>
    {  
        Task<int> IRequestHandler<UpdatePermissionRequest, int>.Handle(UpdatePermissionRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
