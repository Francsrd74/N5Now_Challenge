using AccessControl.Domain.Entities;
using AccessControl.Domain.Interfaces.Permission;
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
    }

    public class UpdatePermissionRequesttHandler : IRequestHandler<UpdatePermissionRequest, int>
    {
        private readonly IPermissionUOW _permissionUOW;
        public UpdatePermissionRequesttHandler(IPermissionUOW permissionUOW)
        {
            this._permissionUOW = permissionUOW;
        }

        async Task<int> IRequestHandler<UpdatePermissionRequest, int>.Handle(UpdatePermissionRequest request, CancellationToken cancellationToken)
        {
            var permission = _permissionUOW.Permissions.Get(request.Id);

            if (permission != null)
            {
                permission.EmployeeSurname = request.EmployeeSurname;
                permission.EmployeeForename = request.EmployeeForename;
                permission.PermissionTypeId = request.PermissionTypeId;     
                permission.PermissionDate = DateTime.Now;


                _permissionUOW.Permissions.Update(permission);

                _permissionUOW.SaveChangesAsync(cancellationToken);


                _permissionUOW.PermissionElastic.Update(permission);

            }


            return await Task.FromResult(permission.Id);
        }
    }
}
