using ApplicationService.Handlers.Queries;
using ApplicationService.Models.Request;
using ApplicationService.Models.Response;
using ElasticPersistence.Abstract;
using ElasticPersistence.Persistence;
using KafkaPersistence;
using KafkaPersistence.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using SqlPersistence.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Handlers.Commands
{
    public class ModifyPermissionQueryCommandHandler : IRequestHandler<ModifyPermissionModel, ModifyPermissionModelResponse>
    {
        private readonly ILogger<ModifyPermissionQueryCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IElasticsearchService _elasticsearchService;
        private readonly IKafkaProducer _kafkaProducer;

        public ModifyPermissionQueryCommandHandler(ILogger<ModifyPermissionQueryCommandHandler> logger, IUnitOfWork unitOfWork, IElasticsearchService elasticsearchService, IKafkaProducer kafkaProducer)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _elasticsearchService = elasticsearchService;
            _kafkaProducer = kafkaProducer;
        }

        public Task<ModifyPermissionModelResponse> Handle(ModifyPermissionModel request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("HttpPost::Post::ModifyPermission::ModifyPermissionModelResponse begin");

            var permissionTypeExist = _unitOfWork.PermissionTypes.Get(request.PermissionType);
            
            var permission = _unitOfWork.Permissions.Get(request.Id);
            if (permission == null || permissionTypeExist == null)
            {
                _logger.LogInformation("HttpPost::Post::ModifyPermission::ModifyPermissionModelResponse end");
                return Task.FromResult(new ModifyPermissionModelResponse
                {
                    Success = false,
                    Message = "Permission or PermissionType not found"
                });
            }
            permission.EmployeeForename = request.EmployeeForename;
            permission.EmployeeSurname = request.EmployeeSurname;
            permission.PermissionType = request.PermissionType;

            _unitOfWork.Complete();

            var msg = new Domain.Models.DTO.MessageDTO { Id = Guid.NewGuid(), NameOperation = "modify" };
            _kafkaProducer.ProduceMessage(msg);
            _elasticsearchService.InsertDocument(permission, msg.Id.ToString());

            _logger.LogInformation("HttpPost::Post::ModifyPermission::ModifyPermissionModelResponse end");
            return Task.FromResult(new ModifyPermissionModelResponse
            {
                Success = true,
                Message = "Permission modified successfully"
            });
        }
    }
}
