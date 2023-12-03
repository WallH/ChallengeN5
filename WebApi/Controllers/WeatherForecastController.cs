using ApplicationService.Models.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SqlPersistence.Abstract;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IUnitOfWork _unitOfWorkTest;
        private readonly IMediator _mediator;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUnitOfWork unitOfWork, IMediator mediator)
        {
            _logger = logger;
            _unitOfWorkTest = unitOfWork;
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var req = new GetPermissionsModel
            {

            };
            var res = await _mediator.Send(req);
            return Ok(res);
        }
    }
}