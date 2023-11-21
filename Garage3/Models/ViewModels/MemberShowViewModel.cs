#nullable disable
using Garage3.Controllers;
using System.ComponentModel;

namespace Garage3.Models.ViewModels
{
    public class MemberShowViewModel
    {
        public int Id { get; set; }


        [DisplayName("Person number")]
        public string PersonNo { get; set; }


        [DisplayName("Fist name")]
        public string FirstName { get; set; }


        [DisplayName("Last name")]
        public string LastName { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }


        [DisplayName("Number of vehicles")]
        public int NoOfVehicles { get; set; }

       
    }
}
