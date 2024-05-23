using ConsoleAppStarter.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;

public class CarClient : ICarClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<CarClient> _logger;

    public CarClient(HttpClient httpClient, ILogger<CarClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<IEnumerable<VehicleTypesMakes>> GetTypesForMake(string carMake)
    {
        _logger.LogInformation("Getting vehicle types for make {CarMake}", carMake);

        var request = new HttpRequestMessage(HttpMethod.Get, $"GetVehicleTypesForMake/{carMake}?format=json");
        var response = await _httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonSerializer.Deserialize<VehicleAPIResponse>(content);

        _logger.LogInformation("Found {Count} vehicle types for make {CarMake}", apiResponse?.Count, carMake);
        return apiResponse?.Results ?? [];
    }
}
