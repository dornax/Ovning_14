using Microsoft.AspNetCore.Mvc.Rendering;

namespace Garage3.Models.ViewModels
{
    public class VehicleTypeViewModel
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }


}
