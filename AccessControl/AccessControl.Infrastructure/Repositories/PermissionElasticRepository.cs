using AccessControl.Domain.Entities;
using AccessControl.Domain.Interfaces;
using AccessControl.Domain.Interfaces.Permission;
using Elastic.Clients.Elasticsearch;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Infrastructure.Repositories
{
    public class PermissionElasticRepository : IPermissionElasticRepository
    {
        private readonly ElasticsearchClient _elasticsearchClient;
        private readonly ILogger<PermissionElasticRepository> _logger;

        private const String PermissionIndex = "permission_index";
        public PermissionElasticRepository(ElasticsearchClient elasticsearchClient, ILogger<PermissionElasticRepository> logger)
        {
            _elasticsearchClient = elasticsearchClient;
            _logger = logger;

            var indexExist = _elasticsearchClient.Indices.ExistsAsync(PermissionIndex).Result;
            if (!indexExist.Exists)
            {
                var createIndexResponse = _elasticsearchClient.Indices.CreateAsync<Permission>(PermissionIndex).Result;

                if (!createIndexResponse.IsValidResponse)
                {
                    _logger.LogError(createIndexResponse.ElasticsearchServerError.Error.Reason);
                    throw new Exception($"could not be index create: {createIndexResponse.ElasticsearchServerError.Error.Reason}");
                }
            }

        }

        async Task<Permission> IRepository<Permission>.AddAsync(Permission entity, CancellationToken cancellationToken)
        {
            var indexResponse = await _elasticsearchClient.CreateAsync<Permission>(entity, i => i.Index(PermissionIndex).Id(entity.Id));

            _logger.LogInformation("AddAsync entity in to elastic for the index {PermissionIndex} from ID: {id}", PermissionIndex, entity.Id);
            return await Task.FromResult<Permission>(entity);
        }

        async Task<Permission> IRepository<Permission>.GetAsync(int id, CancellationToken cancellationToken)
        {

            _logger.LogInformation("GetAsync entity from elastic for the index {PermissionIndex} ID: {id}", PermissionIndex, id);
            var result = await _elasticsearchClient.GetAsync<Permission>(id, g => g.Index(PermissionIndex));

            return await Task.FromResult(result.Source);
        }

        public async Task UpdateAsync(Permission entity, CancellationToken cancellationToken)
        {
            await _elasticsearchClient.UpdateAsync<Permission, Permission>(PermissionIndex, entity.Id, i => i.Doc(entity));
            _logger.LogInformation("UpdateAsync to the entity in elastic for the index {PermissionIndex} whose ID: {id}", PermissionIndex, entity.Id);
        }

    }
}
