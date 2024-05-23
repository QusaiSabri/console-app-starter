using ConsoleAppStarter.Models;
using System.Text.Json;

public class CarClient : ICarClient
{
    private readonly HttpClient _httpClient;

    public CarClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<VehicleTypesMakes>> GetTypesForMake(string carMake)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"GetVehicleTypesForMake/{carMake}?format=json");
        var response = await _httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonSerializer.Deserialize<VehicleAPIResponse>(content);

        return apiResponse?.Results ?? [];
    }
}
