using AccessControl.Application.Common.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Application.Permissions.Queries.GetPermission
{
    public class GetPermissionRequestValidator : AbstractValidator<GetPermissionRequest>
    {
        public GetPermissionRequestValidator(IAccessControlContext _context)
        {
            RuleFor(r => r.Id).NotEmpty().NotNull().WithMessage("The field is empty or null");
                
            RuleFor(r => r.Id).Must(m =>
            {  
                var permission = _context.Permissions.AsNoTracking().FirstOrDefault(f => f.Id.Equals(m));

                return permission != null;

            }).WithMessage("Permission Not Found");  
             
        }
    }

}
