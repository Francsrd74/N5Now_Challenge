using AccessControl.Application.Common.Interfaces;
using AccessControl.Domain.Interfaces.Permission;
using AccessControl.Infrastructure.Repositories;
using Confluent.Kafka;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Snapshot;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Infrastructure.UnitOfWork
{
    public class PermissionUOW : IPermissionUOW
    {
        private readonly IAccessControlContext _accessControlContext;
        private readonly ILogger<IPermissionRepository> _logger;
        private readonly ElasticsearchClient _elasticsearchClient;
        private readonly IProducer<Null, string> _kafkaProducer;


        public PermissionUOW(IAccessControlContext accessControlContext, ElasticsearchClient elasticsearchClient, IProducer<Null, string> kafkaProducer, ILogger<IPermissionRepository> logger)
        {
            this._accessControlContext = accessControlContext;
            this._elasticsearchClient = elasticsearchClient;
            _kafkaProducer = kafkaProducer;
            _logger = logger;
        }

        public IPermissionRepository Permissions => new PermissionRepository(_accessControlContext); 
        public IPermissionRepository PermissionElastic => new PermissionElasticRepository(_elasticsearchClient, _logger); 
        public IPermissionRepository PermissionKafka => new PermisionKafkaRepository(_logger, _kafkaProducer);
         

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
        { 
            return await _accessControlContext.SaveChangesAsync(cancellationToken) > 0;
        }

        public void Dispose()
        {
        }
    }
}
