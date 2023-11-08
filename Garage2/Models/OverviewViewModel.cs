using Garage2.Models.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Garage2.Models
{
    public class OverviewViewModel
    {


        public int ParkedVehicleId { get; set; }


        [DisplayName("Vehicle type")]
        public VehicleTypes VehicleType { get; set; }


        [DisplayName("Registration number")]
        public string RegistrationNumber { get; set; } = string.Empty;
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string? Color { get; set; }


        [DisplayName("Time of arrival")]
        public DateTime TimeOfArrival { get; } = DateTime.Today;

      
        



    }
}
