using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.Api.Controllers
{
    [ApiController]    
    [Route("api/[controller]")]
    public class PermissionController : ControllerBase
    {
        private readonly ILogger<PermissionController> _logger;

        private ISender _mediator;  
        public PermissionController(ILogger<PermissionController> logger, IServiceProvider services)
        {
            _logger = logger;
            _mediator = services.GetService<ISender>(); 
        }

        [HttpGet("{id?}")]
        public async Task<ActionResult> GetPermission(int? id)
        {
            _logger.LogInformation("get permision: {id}", id);  

            return NotFound();
        }
    }
}

 