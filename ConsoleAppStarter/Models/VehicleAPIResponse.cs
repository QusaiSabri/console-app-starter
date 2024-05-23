namespace ConsoleAppStarter.Models
{
    public class VehicleAPIResponse
    {
        public int Count { get; set; }
        public string Message { get; set; }
        public string SearchCriteria { get; set; }
        public IEnumerable<VehicleTypesMakes> Results { get; set; }
    }
}
