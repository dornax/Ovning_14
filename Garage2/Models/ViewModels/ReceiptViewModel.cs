namespace Garage2.Models.ViewModels
{
    public class ReceiptViewModel
    {
        public int ParkedVehicleId { get; set; }
        public string VehicleType { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; } = string.Empty;
 
        public DateTime TimeOfArrival { get; set; } = DateTime.Today;
    }
}
