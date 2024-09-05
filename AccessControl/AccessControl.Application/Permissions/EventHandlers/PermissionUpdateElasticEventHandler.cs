using AccessControl.Application.Common.Interfaces.Services;
using AccessControl.Application.Models;
using AccessControl.Domain.Entities;
using AccessControl.Domain.Events.Permission;
using AccessControl.Domain.Interfaces.Permission;
using Confluent.Kafka;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Application.Permissions.EventHandlers
{
    internal class PermissionUpdateElasticEventHandler : INotificationHandler<DomainEventNotification<PermissionUpdateEvent>>
    {
        public readonly IPermissionElasticRepository _permissionRepository;
        public readonly ILogger<PermissionCreateElasticEventHandler> _logger;
        public PermissionUpdateElasticEventHandler(IPermissionElasticRepository permissionKafkaRepository, ILogger<PermissionCreateElasticEventHandler> logger = null)
        {
            _permissionRepository = permissionKafkaRepository;
            _logger = logger;
        }

        public async Task Handle(DomainEventNotification<PermissionUpdateEvent> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Init PermissionCreateElasticEventHandler");

            await _permissionRepository.UpdateAsync(notification.DomainEvent.permission, cancellationToken);

            _logger.LogInformation("Complete PermissionCreateElasticEventHandler"); 

        }
    }
}
