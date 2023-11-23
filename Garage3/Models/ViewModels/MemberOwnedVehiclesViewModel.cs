using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

#nullable disable
namespace Garage3.Models.ViewModels
{
    public class MemberOwnedVehiclesViewModel
    {
        public int Id { get; set; }

        [DisplayName("Registration number")]
        public string RegistrationNo { get; set; }
        public string Type { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int? Year { get; set; }
        public string Color { get; set; }
        [DisplayName("Number of wheels")]
        public int? NumberOfWheels { get; set; }
        [DisplayName("Time of arrival")]
        public DateTime TimeOfArrival { get; set; }
        //public int MemberId { get; set; }
        //public int? ParkingSpaceId { get; set; }
        //public ParkingSpace ParkingSpace { get; set; }
    }
}
