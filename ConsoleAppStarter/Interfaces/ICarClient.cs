using ConsoleAppStarter.Models;

public interface ICarClient
{
    Task<IEnumerable<VehicleTypesMakes>> GetTypesForMake(string careMake);
}