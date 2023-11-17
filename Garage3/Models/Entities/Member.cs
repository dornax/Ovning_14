namespace Garage3.Models.Entities
{
    public class Member
    {
        public  int Id { get; set; }
        public string PersonNo { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

    }
}
