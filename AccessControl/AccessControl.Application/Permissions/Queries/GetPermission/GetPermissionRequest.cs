using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Application.Permissions.Queries.GetPermission
{
    public class GetPermissionRequest : IRequest<int>
    {
        public int Id { get;set; } 
    }

    public class GetPermissionRequestHandler : IRequestHandler<GetPermissionRequest, int>
    {
        public Task<int> Handle(GetPermissionRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(1);
        }
    }
}
