using AccessControl.Application.Common.DTOs;
using AccessControl.Application.Common.Interfaces.Services;
using AccessControl.Application.Models;
using AccessControl.Domain.Common.Enums;
using AccessControl.Domain.Entities;
using AccessControl.Domain.Events.Permission;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Application.Permissions.EventHandlers
{
    public class PermissionUpdateKafkaEventHandler : INotificationHandler<DomainEventNotification<PermissionUpdateEvent>>
    {

        private readonly IKafkaService<PermissionTopicMessageDto> _permissionKafkaRepository;
        public readonly ILogger<PermissionCreateKafkaEventHandler> _logger;


        public PermissionUpdateKafkaEventHandler(IKafkaService<PermissionTopicMessageDto> kafkaService, ILogger<PermissionCreateKafkaEventHandler> logger)
        {
            this._permissionKafkaRepository = kafkaService;
            _logger = logger;
        }

        public async Task Handle(DomainEventNotification<PermissionUpdateEvent> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Init PermissionUpdateKafkaEventHandler");

            await _permissionKafkaRepository.ProducerTopicAsync<PermissionTopicMessageDto>(new PermissionTopicMessageDto(TopicOperationEnum.Update.ToString()), cancellationToken);

            _logger.LogInformation("Complete PermissionUpdateKafkaEventHandler");

        }
    }
}
