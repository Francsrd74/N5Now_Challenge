using AccessControl.Application.Permissions.Commands.RequestPermission;
using AccessControl.Application.Permissions.Commands.UpdatePermission;
using AccessControl.Application.Permissions.Queries.GetPermission;
using AccessControl.Application.Permissions.Queries.GetPermission.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AccessControl.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController : ControllerBase
    {
        private readonly ILogger<PermissionController> _logger;

        private readonly ISender _mediator;
        public PermissionController(ILogger<PermissionController> logger, ISender mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
         

        [HttpPost("[action]")]
        public async Task<int> RequestPermission([FromBody] RequestPermissioRequest request)
        {
            _logger.LogInformation("Request Permissio: {request}", request);

            return await _mediator.Send(request);
        }

        [HttpPost("[action]")]
        public async Task<int> UpdatePermission([FromBody] UpdatePermissionRequest request)
        {
            _logger.LogInformation("Update Permissio: {request}", request);

            return await _mediator.Send(request);
        }

        [HttpGet("[action]")]
        public async Task<PermissionResponseDto> GetPermission([FromQuery] GetPermissionRequest query)
        {
            _logger.LogInformation("get permision: {id}", query.Id);

            return await _mediator.Send(query);
        }

    }
}

 