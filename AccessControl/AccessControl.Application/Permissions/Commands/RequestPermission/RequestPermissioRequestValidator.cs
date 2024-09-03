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

namespace AccessControl.Application.Permissions.Commands.RequestPermission
{
    public class RequestPermissioRequestValidator : AbstractValidator<RequestPermissioRequest>
    {
        public RequestPermissioRequestValidator(IAccessControlContext _accessControlContext)
        {
            RuleFor(r => r.EmployeeForename).NotEmpty().NotNull();
            RuleFor(r => r.EmployeeSurname).NotEmpty().NotNull();

            RuleFor(r => r.PermissionTypeId).Must(value =>
            {
                return _accessControlContext.PermissionTypes.AsNoTracking().FirstOrDefault(pt => pt.Id.Equals(value)) != null;

            }).WithMessage("PermissionTypeId no exist");

            RuleFor(r => r).Must(value =>
            {
                return _accessControlContext.Permissions.AsNoTracking().FirstOrDefault(p => p.EmployeeForename.Equals(value.EmployeeForename) && 
                p.EmployeeSurname.Equals(value.EmployeeSurname)) == null;

            }).WithMessage("employee already exists");


        } 
    } 
}
