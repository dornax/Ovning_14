﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Garage3.Models.Entities
{
    public class Vehicle
    {
        public  int Id { get; set; }


        [DisplayName("Registration number")]
        [Required]
        //[Remote(action: "IsRegNoAvailable", controller: "ParkedVehicles", ErrorMessage = "Vehicle is already parked in the garage.")]
        [RegularExpression(@"^[A-Za-z]{3}\d{3}$", ErrorMessage = "Registration number must consist of 6 characters, 3 letters followed by 3 digits.")]
        [StringLength(6)]
        public string RegistrationNo { get; set; } = string.Empty;


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
        [Range(0, 24)]
        public int? NumberOfWheels { get; set; }


        [DisplayName("Time of arrival")]
        public DateTime TimeOfArrival { get; set; }

        public int MemberId { get; set; }
        public Member Member { get; set; }
        public int VehicleTypeId { get; set; }
     
        [DisplayName("Vehicle type")]
        [Required]
        public VehicleType VehicleType { get; set; }
        public int? ParkingSpaceId { get; set; }
        public ParkingSpace? ParkingSpace { get; set; }
    }
}
