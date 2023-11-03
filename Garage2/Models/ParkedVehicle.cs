namespace Garage2.Models
{
    public class ParkedVehicle
    {
        
        public int ParkedVehicleId { get; set; }
        public string VehicleType { get; set; } = string.Empty;
        public string RegistrationNumber { get; set;} = string.Empty;
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int? Year { get; set; }
        public string? Color { get; set; }
        public int? NumberOfWheels { get; set; }
        public DateTime TimeOfArrival {get;}
        
    }
}
