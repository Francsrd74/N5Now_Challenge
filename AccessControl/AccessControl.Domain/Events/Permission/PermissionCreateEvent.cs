using AccessControl.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Domain.Events.Permission
{
    public class PermissionCreateEvent : DomainEvent
    {
        public AccessControl.Domain.Entities.Permission permission { get; }
        public PermissionCreateEvent(Entities.Permission permission)
        {
            this.permission = permission;
        } 
    }
}
