using AccessControl.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AccessControl.Domain.Entities
{
    public partial class Permission : IHasDomainEvent
    {
        public Permission()
        {
            DomainEvents = new List<DomainEvent>();
        }

        public int Id { get; set; }
        public string EmployeeForename { get; set; } = null!;
        public string EmployeeSurname { get; set; } = null!;
        public int PermissionTypeId { get; set; }
        public DateTime PermissionDate { get; set; }

        public virtual PermissionType PermissionType { get; set; } = null!;
        [JsonIgnore()]
        public List<DomainEvent> DomainEvents { get; }
    }
}
