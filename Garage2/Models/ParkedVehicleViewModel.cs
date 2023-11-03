using System.ComponentModel.DataAnnotations;

namespace Garage2.Models
{
    public class ParkedVehicleViewModel
    {
        [RegularExpression(@"^[A-Za-z]{3}\d{3}$", ErrorMessage = "Invalid registration number")]
        string RegNo { get; set; }
        public DateTime TimeOfArrival { get; } = DateTime.Today;
        public string Type { get; set; } = string.Empty;
        public int? NumberOfWheels { get; set; }
        public string? Color { get; set; }
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;



    }
}
