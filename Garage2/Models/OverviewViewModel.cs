using System.ComponentModel.DataAnnotations;

namespace Garage2.Models
{
    public class OverviewViewModel
    {


        public int ParkedVehicleId { get; set; }
        public string VehicleType { get; set; } = string.Empty;

        public string RegistrationNumber { get; set; } = string.Empty;
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string? Color { get; set; }
        public DateTime TimeOfArrival { get; } = DateTime.Today;

      
        



    }
}
