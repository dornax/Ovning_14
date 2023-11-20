#nullable disable
using Garage3.Controllers;

namespace Garage3.Models.ViewModels
{
    public class MemberShowViewModel
    {
        public int Id { get; set; }
        public string PersonNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
        public int NoOfVehicles { get; set; }

        public void ClaculateNoOfVehicles()
        {
            NoOfVehicles = Vehicles.Count;
        }
    }
}
