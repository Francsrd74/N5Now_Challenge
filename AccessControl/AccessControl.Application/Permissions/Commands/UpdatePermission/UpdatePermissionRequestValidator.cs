using AccessControl.Application.Common.Interfaces;
using AccessControl.Domain.Entities;
using AccessControl.Domain.Interfaces.Permission;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Application.Permissions.Commands.UpdatePermission
{
    public class UpdatePermissionRequestValidator : AbstractValidator<UpdatePermissionRequest>
    { 
        public UpdatePermissionRequestValidator(IAccessControlContext _accessControlContext)
        {
            RuleFor(r => r.EmployeeForename).NotEmpty().NotNull();
            RuleFor(r => r.EmployeeSurname).NotEmpty().NotNull();

            RuleFor(r => r.PermissionTypeId).Must(value =>
            {
                return _accessControlContext.PermissionTypes.AsNoTracking().FirstOrDefault(pt => pt.Id.Equals(value)) != null;

            }).WithMessage("PermissionTypeId no exist");

            RuleFor(r => r.Id).Must(m =>
            {
                return _accessControlContext.Permissions.AsNoTracking().FirstOrDefault(f => f.Id.Equals(m)) != null; 

            }).WithMessage("Permission Not Found");
        }
    }
}
