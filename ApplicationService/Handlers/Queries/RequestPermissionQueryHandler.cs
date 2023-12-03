using ApplicationService.Models.Request;
using ApplicationService.Models.Response;
using Domain.Entities;
using Domain.Models;
using ElasticPersistence.Abstract;
using KafkaPersistence.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using SqlPersistence.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Handlers.Queries
{
    public class RequestPermissionQueryHandler : IRequestHandler<RequestPermissionModel, RequestPermissionModelResponse>
    {
        private readonly ILogger<RequestPermissionQueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IElasticsearchService _elasticsearchService;
        private readonly IKafkaProducer _kafkaProducer;
        public RequestPermissionQueryHandler(ILogger<RequestPermissionQueryHandler> logger, IUnitOfWork unitOfWork, IElasticsearchService elasticsearchService, IKafkaProducer kafkaProducer)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _elasticsearchService = elasticsearchService;
            _kafkaProducer = kafkaProducer;
        }

        public Task<RequestPermissionModelResponse> Handle(RequestPermissionModel request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("HttpGet::Get::RequestPermission::RequestPermissionModelResponse begin");
            var permission = _unitOfWork.Permissions.Get(request.Id);
            if(permission == null)
            {
                _logger.LogInformation("HttpGet::Get::RequestPermission::RequestPermissionModelResponse end");
                return Task.FromResult(new RequestPermissionModelResponse
                {
                    Success = false
                });
            }
            var response = new RequestPermissionModelResponse
            {
                Permission = new PermissionModel
                {
                    Id = permission.Id,
                    EmployeeForename = permission.EmployeeForename,
                    EmployeeSurname = permission.EmployeeSurname,
                    PermissionType = new PermissionType
                    {
                        Id = permission.PermissionType,
                        Description = ""
                    },
                    PermissionDate = permission.PermissionDate
                }
            };

            var msg = new Domain.Models.DTO.MessageDTO { Id = Guid.NewGuid(), NameOperation = "request" };
            _kafkaProducer.ProduceMessage(msg);
            _elasticsearchService.InsertDocument(permission, msg.Id.ToString());

            _logger.LogInformation("HttpGet::Get::RequestPermission::RequestPermissionModelResponse end");
            return Task.FromResult(response);
        }
    }
}
