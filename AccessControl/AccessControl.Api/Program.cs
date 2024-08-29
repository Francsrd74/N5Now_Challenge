using AccessControl.Api;

internal class Program
{
    private static void Main(string[] args)
    {
        //var configuration = new ConfigurationBuilder()
        //    .AddJsonFile("appsettings.json")
        //    .Build(); 

        try
        { 
            CreateHostBuilder(args).Build().Run();
        }
        catch (Exception ex)
        { 
        } 
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<StartUp>(); 
            });
}