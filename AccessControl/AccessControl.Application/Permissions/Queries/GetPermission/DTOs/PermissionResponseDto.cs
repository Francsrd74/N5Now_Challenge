using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Application.Permissions.Queries.GetPermission.DTOs
{
    public class PermissionResponseDto
    { 
        public int Id { get; set; }
        public string EmployeeForename { get; set; } = null!;
        public string EmployeeSurname { get; set; } = null!;
        public int PermissionTypeId { get; set; }
        public DateTime PermissionDate { get; set; }
    }
}
