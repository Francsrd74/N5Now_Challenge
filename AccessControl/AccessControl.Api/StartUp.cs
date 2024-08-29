using AccessControl.Infrastructure;

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
            // Add services to the container.

            services.AddControllers();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Add Infra
            services.AddInfrastructure(Configuration); 
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
             
              
        }
    }
}
