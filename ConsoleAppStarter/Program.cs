using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var hostBuilder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false)
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
    }).Build();

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