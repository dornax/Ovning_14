using Garage2.Models.Entities;

namespace Garage2.Models.ViewModels
{
    public class ReceiptViewModel
    {
        public int ParkedVehicleId { get; set; }
        public VehicleTypes VehicleType { get; set; }
        public string RegistrationNumber { get; set; } = string.Empty;

        public DateTime TimeOfArrival { get; set; } = DateTime.Today;

        public DateTime TimeOfDeparture { get; set; } = DateTime.Today;

        public decimal Price { get; set; }

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
