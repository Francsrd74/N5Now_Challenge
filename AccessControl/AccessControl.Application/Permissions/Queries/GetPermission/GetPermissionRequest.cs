using AccessControl.Application.Common.Interfaces;
using AccessControl.Application.Permissions.Queries.GetPermission.DTOs;
using AccessControl.Domain.Events.Permission;
using AccessControl.Domain.Interfaces.Permission;
using Confluent.Kafka;
using Elastic.Clients.Elasticsearch;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AccessControl.Application.Permissions.Queries.GetPermission
{
    public class GetPermissionRequest : IRequest<PermissionResponseDto>
    {
        public int Id { get;set; } 
    }

    public class GetPermissionRequestHandler : IRequestHandler<GetPermissionRequest, PermissionResponseDto>
    {
        private readonly IPermissionElasticRepository _permissionElasticRepository; 
        private readonly IDomainEventService _domainEventService; 
        private readonly ILogger<GetPermissionRequestHandler> _logger;
        public GetPermissionRequestHandler(IPermissionElasticRepository permissionElasticRepository, ILogger<GetPermissionRequestHandler> logger, IDomainEventService domainEventService)
        {
            _permissionElasticRepository = permissionElasticRepository; 
            _logger = logger;
            _domainEventService = domainEventService;
        }

        public async Task<PermissionResponseDto> Handle(GetPermissionRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Init GetPermissionRequestHandler");
            ///get permision from Elastic
            var result = await _permissionElasticRepository.GetAsync(request.Id, cancellationToken);

            ///Publish domainEvent Get  
            await _domainEventService.Publish(new PermissionGetEvent(result));


            PermissionResponseDto? employee = null;

            if (result != null) {

                employee = new PermissionResponseDto()
                {
                    Id = result.Id,
                    EmployeeForename = result.EmployeeForename,
                    EmployeeSurname = result.EmployeeSurname,
                    PermissionTypeId = result.PermissionTypeId,
                    PermissionDate = result.PermissionDate,
                };
                 
            } 

            _logger.LogInformation("Complete GetPermissionRequestHandler");
             
            return employee;
        }
    }
}
