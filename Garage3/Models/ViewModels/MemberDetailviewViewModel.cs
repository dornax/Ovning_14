#nullable disable
namespace Garage3.Models.ViewModels
{
    public class MemberDetailviewViewModel
    {
        public int Id { get; set; }
        public string PersonNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
