using AccessControl.Application.Common.DTOs;
using AccessControl.Application.Common.Interfaces.Services;
using AccessControl.Domain.Common;
using AccessControl.Domain.Entities;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AccessControl.Infrastructure.Repositories
{
    internal class PermissionKafkaService : IKafkaService<PermissionTopicMessageDto>
    {
        private readonly ILogger<PermissionKafkaService> _logger;
        private readonly IProducer<Null, string> _producerBuilder;
        private const string _create_topic = "permission-topic"; 

        public PermissionKafkaService(ILogger<PermissionKafkaService> logger, IProducer<Null, string> producerBuilder)
        {
            _logger = logger;
            _producerBuilder = producerBuilder;
        }
           
        public async Task<TSource> ProducerTopicAsync<TSource>(TSource entity, CancellationToken cancellationToken)  
        {
            _logger.LogInformation("ProducerTopicAsync: {topic}", _create_topic);

             
            var jsonSerialOptions = new JsonSerializerSettings {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var message = new Message<Null, string>
            {
                Value = JsonConvert.SerializeObject(entity, jsonSerialOptions)
            };

            var result = await _producerBuilder.ProduceAsync(_create_topic, message, cancellationToken);

            var unserialObj = JsonConvert.DeserializeObject<TSource>(result.Value, jsonSerialOptions);

            _logger.LogInformation("Succes ProducerTopicAsync: {topic}", _create_topic);


            return entity;
        }
    }
}
