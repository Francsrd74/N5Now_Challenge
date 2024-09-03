using AccessControl.Application.Permissions.Queries.GetPermission.DTOs;
using AccessControl.Domain.Interfaces.Permission;
using MediatR;

namespace AccessControl.Application.Permissions.Queries.GetPermission
{
    public class GetPermissionRequest : IRequest<PermissionResponseDto>
    {
        public int Id { get;set; } 
    }

    public class GetPermissionRequestHandler : IRequestHandler<GetPermissionRequest, PermissionResponseDto>
    {
        private readonly IPermissionUOW _permissionUOW;
        public GetPermissionRequestHandler(IPermissionUOW permissionUOW)
        {
            _permissionUOW = permissionUOW;
        }

        public Task<PermissionResponseDto> Handle(GetPermissionRequest request, CancellationToken cancellationToken)
        {
            var result = _permissionUOW.PermissionElastic.Get(request.Id);

            if (result != null) {

                var employee = new PermissionResponseDto()
                {
                    Id = result.Id,
                    EmployeeForename = result.EmployeeForename,
                    EmployeeSurname = result.EmployeeSurname,
                    PermissionTypeId = result.PermissionTypeId,
                    PermissionDate = result.PermissionDate,
                };
                
                return Task.FromResult(employee);
            }


            return null;
        }
    }
}
