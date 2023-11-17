namespace Garage3.Models.Entities
{
    public class VehicleType
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
