using AccessControl.Application.Common.DTOs;
using AccessControl.Application.Common.Interfaces;
using AccessControl.Application.Common.Interfaces.Services;
using AccessControl.Domain.Entities;
using AccessControl.Domain.Interfaces.Permission;
using AccessControl.Infrastructure.Clients.Elastic;
using AccessControl.Infrastructure.Clients.Kafka;
using AccessControl.Infrastructure.Persistence;
using AccessControl.Infrastructure.Repositories;
using AccessControl.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AccessControl.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDomainEventService, DomainEventService>();
            services.AddDbContext<AccessControlContext>(options =>
              options.UseSqlServer(
                  configuration.GetConnectionString("EFCoreDataBase"),
                  b => b.MigrationsAssembly(typeof(AccessControlContext).Assembly.FullName)));

            services.AddScoped<IAccessControlContext>(provider => provider.GetService<AccessControlContext>()); 
            services.AddTransient<IKafkaService<PermissionTopicMessageDto>, PermissionKafkaService>();
            services.AddTransient<IPermissionElasticRepository, PermissionElasticRepository>(); 
            services.AddTransient<IPermissionEntityFrameworkRepository, PermissionEntityFrameworkRepository>(); 

            //AddAsync ElasticSearch
            services.AddElasticSearch(configuration);

            //AddAsync kafka Client
            services.AddKafka(configuration);
             

            return services;

        }
    }
}
