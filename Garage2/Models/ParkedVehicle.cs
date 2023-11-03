using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Garage2.Models
{
    public class ParkedVehicle
    {
        [Key]
        [DisplayName("ID")]
        public int ParkedVehicleId { get; set; }



        [DisplayName("Vehicle type")]
        [Required]
        public string VehicleType { get; set; } = string.Empty;



        [DisplayName("Registration number")]
        [Required]
        [RegularExpression(@"^[A-Za-z]{3}\d{3}$", ErrorMessage = "Registration number must consist of 6 characters, 3 letters followed by 3 digits.")]
        [StringLength(6, ErrorMessage = "Registration number must consist of 6 characters, 3 letters followed by 3 digits.")]
        public string RegistrationNumber { get; set;} = string.Empty;



        [StringLength(25, ErrorMessage = "")]
        [Required]
        public string Make { get; set; } = string.Empty;



        [StringLength(25, ErrorMessage = "")]
        [Required]
        public string Model { get; set; } = string.Empty;



        [Range(1930, 3000, ErrorMessage = "")]
        public int? Year { get; set; }



        [StringLength(25, ErrorMessage = "")]
        public string? Color { get; set; }



        [DisplayName("Number of wheels")]
        [Range(0,16, ErrorMessage = "")]
        public int? NumberOfWheels { get; set; }



        [DisplayName("Time of arrival")]
        public DateTime TimeOfArrival { get; set; } = DateTime.Today;

    }
}
