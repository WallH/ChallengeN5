using ApplicationService.Models.Request;
using ApplicationService.Models.Response;
using Azure;
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
    public class GetPermissionsQueryHandler : IRequestHandler<GetPermissionsModel, GetPermissionsModelResponse>
    {
        private readonly ILogger<GetPermissionsQueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IElasticsearchService _elasticsearchService;
        private readonly IKafkaProducer _kafkaProducer;
        public GetPermissionsQueryHandler(ILogger<GetPermissionsQueryHandler> logger, IUnitOfWork unitOfWork, IElasticsearchService elasticsearchService, IKafkaProducer kafkaProducer)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _elasticsearchService = elasticsearchService;
            _kafkaProducer = kafkaProducer;
        }
        public async Task<GetPermissionsModelResponse> Handle(GetPermissionsModel request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("HttpGet::Get::GetPermissions::GetPermissionsQueryHandler begin");

            var permissions = _unitOfWork.Permissions.GetAll();
            if (permissions == null)
            {
                return new GetPermissionsModelResponse
                {
                    Permissions = new List<PermissionModel>()
                };
            }
            var response = new GetPermissionsModelResponse
            {
                Permissions = permissions.Select(p => new PermissionModel
                {
                    Id = p.Id,
                    EmployeeForename = p.EmployeeForename,
                    EmployeeSurname = p.EmployeeSurname,
                    PermissionType = new PermissionType
                    {
                        Id = p.PermissionType,
                        Description = ""
                    },
                    PermissionDate = p.PermissionDate
                })
            };



            var msg = new Domain.Models.DTO.MessageDTO { Id = Guid.NewGuid(), NameOperation = "get" };
            _kafkaProducer.ProduceMessage(msg);
            _elasticsearchService.InsertDocuments(permissions, msg.Id.ToString());

            _logger.LogInformation("HttpGet::Get::GetPermissions::GetPermissionsQueryHandler end");

            return await Task.FromResult(response);
        }
    }
}
