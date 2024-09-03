using AccessControl.Domain.Entities;
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
    public class PermissionElasticRepository : IPermissionRepository
    {
        private readonly ElasticsearchClient _elasticsearchClient;
        private readonly ILogger<IPermissionRepository> _logger;   

        private const String PermissionIndex = "permission_index";
        public PermissionElasticRepository(ElasticsearchClient elasticsearchClient, ILogger<IPermissionRepository> logger)
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

        public Permission Add(Permission entity)
        {
            var indexResponse = _elasticsearchClient.CreateAsync<Permission>(entity, i => i.Index(PermissionIndex).Id(entity.Id));

            _logger.LogInformation("Add entity in to elastic for the index {PermissionIndex} from ID: {id}", PermissionIndex, entity.Id);
            return entity;
        }

        public Permission Get(int id)
        {

            var result = _elasticsearchClient.GetAsync<Permission>(id, g => g.Index(PermissionIndex)).Result.Source;
            _logger.LogInformation("Get entity from elastic for the index {PermissionIndex} ID: {id}", PermissionIndex, id);

            return result;
        }

        public void Update(Permission entity)
        { 
            _elasticsearchClient.UpdateAsync<Permission, Permission>(PermissionIndex, entity.Id, i => i.Doc(entity)); 
            _logger.LogInformation("Update to the entity in elastic for the index {PermissionIndex} whose ID: {id}", PermissionIndex, entity.Id);
        }
    }
}
