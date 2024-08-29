using System;
using System.Collections.Generic;

namespace AccessControl.Domain.Entities
{
    public partial class Permission
    {
        public int Id { get; set; }
        public string EmployeeForename { get; set; } = null!;
        public string EmployeeSurname { get; set; } = null!;
        public int PermissionTypeId { get; set; }
        public DateTime PermissionDate { get; set; }

        public virtual PermissionType PermissionType { get; set; } = null!;
    }
}
