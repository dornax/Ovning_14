using Microsoft.EntityFrameworkCore;

namespace Garage2.Data
{
    public class Garage2Context : DbContext
    {
        public Garage2Context (DbContextOptions<Garage2Context> options)
            : base(options)
        {
        }

        public DbSet<Garage2.Models.ParkedVehicle> ParkedVehicle { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ParkedVehicle>().HasData(
                new ParkedVehicle { ParkedVehicleId = 1, VehicleType = VehicleTypes.Car, RegistrationNumber = "ABC123", Make = "Volvo", Model = "V70", Year = 2022, Color = "White", NumberOfWheels = 4, TimeOfArrival = DateTime.Now - TimeSpan.FromHours(1.25) },
                new ParkedVehicle { ParkedVehicleId = 2, VehicleType = VehicleTypes.Airplane, RegistrationNumber = "BCD234", Make = "Saab", Model = "Airbus", Year = 2011, Color = "White", NumberOfWheels = 5, TimeOfArrival = DateTime.Now - TimeSpan.FromHours(1.50) },
                new ParkedVehicle { ParkedVehicleId = 3, VehicleType = VehicleTypes.Airplane, RegistrationNumber = "CDE345", Make = "Boeing", Model = "747", Year = 1970, Color = "Silver", NumberOfWheels = 18, TimeOfArrival = DateTime.Now - TimeSpan.FromHours(1.50) },
                new ParkedVehicle { ParkedVehicleId = 4, VehicleType = VehicleTypes.Boat, RegistrationNumber = "DEF456", Make = "Yamaha", Model = "AR210", Year = 2021, Color = "Red", NumberOfWheels = 0, TimeOfArrival = DateTime.Now - TimeSpan.FromHours(1.75) },
                new ParkedVehicle { ParkedVehicleId = 5, VehicleType = VehicleTypes.Bus, RegistrationNumber = "GHI789", Make = "Mercedes-Benz", Model = "Citaro", Year = 2018, Color = "Green", NumberOfWheels = 6, TimeOfArrival = DateTime.Now - TimeSpan.FromHours(2.0) },
                new ParkedVehicle { ParkedVehicleId = 6, VehicleType = VehicleTypes.Car, RegistrationNumber = "JKL012", Make = "Toyota", Model = "Camry", Year = 2019, Color = "Black", NumberOfWheels = 4, TimeOfArrival = DateTime.Now - TimeSpan.FromHours(2.25) },
                new ParkedVehicle { ParkedVehicleId = 7, VehicleType = VehicleTypes.Motorcycle, RegistrationNumber = "MNO345", Make = "Harley-Davidson", Model = "Iron 883", Year = 2020, Color = "Blue", NumberOfWheels = 2, TimeOfArrival = DateTime.Now - TimeSpan.FromHours(2.50) }
            );
        }
    }
}
