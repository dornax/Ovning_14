namespace Garage3.Models.Entities
{
    public class ParkingSpace
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public bool InUse { get; set; }

        public Vehicle Vehicles { get; set; } = new Vehicle();
    }
}
