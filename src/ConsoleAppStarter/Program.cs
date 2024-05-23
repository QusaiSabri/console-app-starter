using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var hostBuilder = CreateHostBuilder(args).Build();

using var scope = hostBuilder.Services.CreateScope();

var services = scope.ServiceProvider;

try
{
    var app = services.GetRequiredService<App>();
    await app.Run(args);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    throw;
}

static IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
        .UseSerilog((context, services, configuration) => configuration
            .ReadFrom.Configuration(context.Configuration))
        .ConfigureAppConfiguration((context, config) =>
        {
            var env = context.HostingEnvironment;

            config.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
        })
        .ConfigureServices((context, services) =>
        {
            services.AddHttpClient<ICarClient, CarClient>(client =>
            {
                var carsApiUrl = context.Configuration.GetSection("Values").GetValue<string>("CARS_API_URL");
                client.BaseAddress = new Uri(carsApiUrl);
            });
            services.AddSingleton<App>();
        });
}