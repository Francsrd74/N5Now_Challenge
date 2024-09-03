using AccessControl.Application.Common.Interfaces;
using AccessControl.Domain.Interfaces.Permission;
using AccessControl.Infrastructure.Clients.Elastic;
using AccessControl.Infrastructure.Clients.Kafka;
using AccessControl.Infrastructure.Persistence;
using AccessControl.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AccessControl.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AccessControlContext>(options =>
              options.UseSqlServer(
                  configuration.GetConnectionString("EFCoreDataBase"),
                  b => b.MigrationsAssembly(typeof(AccessControlContext).Assembly.FullName)));

            services.AddScoped<IAccessControlContext>(provider => provider.GetService<AccessControlContext>());
            services.AddTransient<IPermissionUOW, PermissionUOW>();
            //Add ElasticSearch
            services.AddElasticSearch(configuration);

            //Add kafka Client
            services.AddKafka(configuration);
             

            return services;

        }
    }
}
