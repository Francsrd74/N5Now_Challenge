using AccessControl.Application;
using AccessControl.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;

namespace AccessControl.Api
{
    public class StartUp
    { 
        public StartUp(IConfiguration configuration )
        {
            Configuration = configuration; 
        }

        public IConfiguration Configuration { get; } 


        public void ConfigureServices(IServiceCollection services)
        {
             
            // AddAsync services to the container.
            services.AddControllers();
            services.AddHttpContextAccessor();

            // add application 
            services.AddApplication(); 
            // AddAsync Infra
            services.AddInfrastructure(Configuration);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

                // Configure the HTTP request pipeline.
                if (env.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();

                app.UseAuthorization();
             
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("AllowSpecificOrigins");
             

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            

        }
    }
}
