using AccessControl.Infrastructure.Persistence;
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

            return services;

        }
    }
}
