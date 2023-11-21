#nullable disable
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Garage3.Models.ViewModels
{
    public class MembersEditNewViewModel
    {
        public int Id { get; set; }


        [Required]
        [DisplayName("Person number")]
        public string PersonNo { get; set; }


        [Required]
        [DisplayName("Fist name")]
        public string FirstName { get; set; }


        [Required]
        [DisplayName("Last name")]
        //[Compare(nameof(FirstName))]
        public string LastName { get; set; }
    }
}
