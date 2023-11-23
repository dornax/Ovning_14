using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Garage3.Models.Entities
{
    public class ParkingSpace
    {
        public int Id { get; set; }
        [DisplayName("Parking space")]
        [StringLength(20)]
        public string Name { get; set; } = string.Empty;
        [StringLength(20)]
        public string Level { get; set; } = string.Empty;
        public bool InUse { get; set; }

        public Vehicle Vehicles { get; set; } = new Vehicle();
    }
}
