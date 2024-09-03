using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Infrastructure.Clients.Elastic
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddElasticSearch(this IServiceCollection service, IConfiguration configuration)
        {

            var settings = new ElasticsearchClientSettings(new Uri(configuration.GetRequiredSection("Elasticsearch:uri").Value));

            var client = new ElasticsearchClient(settings);

            service.AddSingleton<ElasticsearchClient>(client);

            return service;
        }
    }
}
