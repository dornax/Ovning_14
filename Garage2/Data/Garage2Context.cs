using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Garage2.Models;
using Garage2.Models.ViewModels;
using Garage2.Models.Entities;

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
                new ParkedVehicle
                {
                    ParkedVehicleId = 1,
                    VehicleType = VehicleTypes.Car,
                    RegistrationNumber = "ABC123",
                    Make = "Volvo",
                    Model = "V70",
                    Year = 2022,
                    Color = "White",
                    NumberOfWheels = 4,
                    TimeOfArrival = DateTime.Now - TimeSpan.FromHours(1.25)
                }
            );
        }
    }
}
