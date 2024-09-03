using Confluent.Kafka;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Infrastructure.Clients.Kafka
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddKafka(this IServiceCollection service, IConfiguration configuration)
        { 
            var settings = new ProducerConfig()
            {
                BootstrapServers = configuration.GetRequiredSection("kafkaClient:uri").Value
            };

            var client = new ProducerBuilder<Null, string>(settings).Build();

            service.AddSingleton<IProducer<Null, string>>(client);  


            return service;
        }
    }
}
