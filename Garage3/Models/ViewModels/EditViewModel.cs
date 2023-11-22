using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Garage3.Models.ViewModels
{
    public class EditViewModel
    {
        public int ParkedVehicleId { get; set; }

        [DisplayName("Vehicle type")]
        [Required]
        public VehicleType VehicleType { get; set; } = new VehicleType();   

        [DisplayName("Registration number")]
        [Required]
        [RegularExpression(@"^[A-Za-z]{3}\d{3}$", ErrorMessage = "Registration number must consist of 6 characters, 3 letters followed by 3 digits.")]
        [StringLength(6)]
        public string RegistrationNumber { get; set; } = string.Empty;

        public string ExistingRegNo { get; set; } = string.Empty;

        [StringLength(25)]
        [Required]
        public string Make { get; set; } = string.Empty;

        [StringLength(25)]
        [Required]
        public string Model { get; set; } = string.Empty;

        [Range(1930, 3000)]
        public int? Year { get; set; }

        [StringLength(25)]
        public string? Color { get; set; }

        [DisplayName("Number of wheels")]
        [Range(0, 16)]
        public int? NumberOfWheels { get; set; }




    }
}
