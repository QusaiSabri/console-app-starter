using Microsoft.Extensions.Logging;

public class App
{
    private readonly ICarClient _carClient;
    private readonly ILogger<App> _logger;

    public App(ICarClient carClient, ILogger<App> logger)
    {
        _carClient = carClient;
        _logger = logger;
    }

    public async Task Run(string[] args)
    {
        _logger.LogInformation("Application is starting.");

        var response = await _carClient.GetTypesForMake("merc");

        if (response == null)
        {
            Console.WriteLine("No car data found");
            return;
        }

        foreach (var carType in response)
        {
            Console.WriteLine($"Make: {carType.MakeName}, Type: {carType.VehicleTypeName}");
        }

        _logger.LogInformation("Application is shutting down.");
    }
}