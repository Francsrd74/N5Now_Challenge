using AccessControl.Domain.Entities;
using AccessControl.Domain.Interfaces;
using AccessControl.Domain.Interfaces.Permission;
using Confluent.Kafka;
using Elastic.Clients.Elasticsearch.Snapshot;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Infrastructure.Repositories
{
    internal class PermissionKafkaRepository : IPermissionRepository
    {
        private readonly ILogger<IPermissionRepository> _logger;
        private readonly IProducer<Null, string> _producerBuilder;
        private const string _topic = "permission-topic";

        public PermissionKafkaRepository(ILogger<IPermissionRepository> logger, IProducer<Null, string> producerBuilder)
        {
            _logger = logger;
            _producerBuilder = producerBuilder;
        }
        public Permission Add(Permission entity)
        {
            var message = new Message<Null, string>
            {
                Value = JsonConvert.SerializeObject(entity)
            };

            var result = _producerBuilder.ProduceAsync(_topic, message).Result.Value;

            var unserial = JsonConvert.DeserializeObject<Permission>(result);

            return unserial;
        }

        public Permission Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Permission entity)
        {
            throw new NotImplementedException();
        }
    }
}
