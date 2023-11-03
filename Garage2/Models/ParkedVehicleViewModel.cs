namespace Garage2.Models
{
    public class ParkedVehicleViewModel
    {
        string RegNo { get; set; }
        public DateTime TimeOfArrival { get; } = DateTime.Today;
        public string Type { get; set; } = string.Empty;



    }
}
