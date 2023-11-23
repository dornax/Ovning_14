using Garage3.Validations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Garage3.Models.Entities
{
    public class Member
    {
        public  int Id { get; set; }


        [Required]
        [DisplayName("Person number")]
        public string PersonNo { get; set; } = string.Empty;


        [Required]
        [DisplayName("Fist name")]
        public string FirstName { get; set; } = string.Empty;


        [Required]
        [DisplayName("Last name")]
        [DifferentValuesValidation(nameof(FirstName))]
        public string LastName { get; set; } = string.Empty;



        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

    }
}
