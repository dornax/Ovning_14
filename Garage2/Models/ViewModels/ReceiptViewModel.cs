using Garage2.Models.Entities;
using System.ComponentModel;

namespace Garage2.Models.ViewModels
{
    public class ReceiptViewModel
    {
        public int ParkedVehicleId { get; set; }


        [DisplayName("Vehicle type")]
        public VehicleTypes VehicleType { get; set; }


        [DisplayName("Registration number")]
        public string RegistrationNumber { get; set; } = string.Empty;


        [DisplayName("Time of arrival")]
        public DateTime TimeOfArrival { get; set; } = DateTime.Today;


        [DisplayName("Time of departure")]
        public DateTime TimeOfDeparture { get; set; } = DateTime.Today;

        public decimal Price { get; set; }

        [DisplayName("Time parked")]
        public string TimeParked { get; set; } = string.Empty;


        public void SetDepartureTime() //kanske kalla på denna genom en knapp när man lämnar parkering?
        {
            TimeOfDeparture = DateTime.Now;
        }

        public void CalculateTimeAndPrice()
        {
            TimeSpan timeParked = TimeOfDeparture - TimeOfArrival;
            TimeParked = timeParked.ToString(@"h\:mm\:ss");
            //if price is 10 kr per hour.
            Price = Math.Round((decimal)timeParked.TotalHours * 10, 2);
        }

    }
}
