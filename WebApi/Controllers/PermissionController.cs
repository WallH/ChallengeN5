using ApplicationService.Models.Request;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlPersistence.Abstract;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly ILogger<PermissionController> _logger;
        private readonly IUnitOfWork _unitOfWorkTest;
        private readonly IMediator _mediator;
        public PermissionController(ILogger<PermissionController> logger, IUnitOfWork unitOfWork, IMediator mediator)
        {
            _logger = logger;
            _unitOfWorkTest = unitOfWork;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            _logger.LogInformation("HttpGet::Get::GetPermissions begin");
            var req = new GetPermissionsModel
            {

            };
            var res = await _mediator.Send(req);
            _logger.LogInformation("HttpGet::Get::GetPermissions end");

            return Ok(res.Permissions);
        }

        [HttpGet("Request/{id}")]
        public async Task<ActionResult> Get(int id)
        {
            _logger.LogInformation("HttpGet::Request/Get(id)::RequestPermission begin");
            var req = new RequestPermissionModel
            {
                Id = id
            };
            var res = await _mediator.Send(req);
            _logger.LogInformation("HttpGet::Request/Get(id)::RequestPermission end");
            return Ok(res.Permission);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> Post(int id, [FromBody] ModifyPermissionModel model)
        {
            _logger.LogInformation("HttpPost::Post(id)::ModifyPermissions begin");

            var req = new ModifyPermissionModel
            {
                Id = id,
                EmployeeForename = model.EmployeeForename,
                EmployeeSurname = model.EmployeeSurname,
                PermissionType = model.PermissionType
            };
            var res = await _mediator.Send(req);
            _logger.LogInformation("HttpPost::Post(id)::ModifyPermissions end");

            return Ok(res);
        }

    }
}
