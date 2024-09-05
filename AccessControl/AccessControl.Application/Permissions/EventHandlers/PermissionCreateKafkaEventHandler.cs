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
    public class PermissionCreateKafkaEventHandler : INotificationHandler<DomainEventNotification<PermissionCreateEvent>>
    {

        private readonly IKafkaService<PermissionTopicMessageDto> _permissionKafkaRepository;
        public readonly ILogger<PermissionCreateKafkaEventHandler> _logger;


        public PermissionCreateKafkaEventHandler(IKafkaService<PermissionTopicMessageDto> kafkaService, ILogger<PermissionCreateKafkaEventHandler> logger)
        {
            this._permissionKafkaRepository = kafkaService;
            _logger = logger;
        }

        public async Task Handle(DomainEventNotification<PermissionCreateEvent> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Init PermissionCreateKafkaEventHandler");

            await _permissionKafkaRepository.ProducerTopicAsync<PermissionTopicMessageDto>(new PermissionTopicMessageDto(TopicOperationEnum.Create.ToString()), cancellationToken);

            _logger.LogInformation("Complete PermissionCreateKafkaEventHandler");

        }
    }
}
