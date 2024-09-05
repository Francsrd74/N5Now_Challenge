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
    public class PermissionGetKafkaEventHandler : INotificationHandler<DomainEventNotification<PermissionGetEvent>>
    {

        private readonly IKafkaService<PermissionTopicMessageDto> _permissionKafkaRepository;
        public readonly ILogger<PermissionCreateKafkaEventHandler> _logger;


        public PermissionGetKafkaEventHandler(IKafkaService<PermissionTopicMessageDto> kafkaService, ILogger<PermissionCreateKafkaEventHandler> logger)
        {
            this._permissionKafkaRepository = kafkaService;
            _logger = logger;
        }

        public async Task Handle(DomainEventNotification<PermissionGetEvent> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Init PermissionGetKafkaEventHandler");

            await _permissionKafkaRepository.ProducerTopicAsync<PermissionTopicMessageDto>(new PermissionTopicMessageDto(TopicOperationEnum.Get.ToString()), cancellationToken);

            _logger.LogInformation("Complete PermissionGetKafkaEventHandler");

        }
    }
}
