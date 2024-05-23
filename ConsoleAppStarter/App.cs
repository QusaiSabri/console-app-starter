public class App
{
    private readonly ICarClient _carClient;

    public App(ICarClient carClient)
    {
        _carClient = carClient;
    }

    public async Task Run(string[] args)
    {
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
    }
}