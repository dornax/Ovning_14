using Microsoft.EntityFrameworkCore;

namespace Garage3.Data
{
    public class Garage3Context : DbContext
    {
        public Garage3Context(DbContextOptions<Garage3Context> options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; } = default!;
        public DbSet<Member> Members { get; set; } = default!;
        public DbSet<ParkingSpace> ParkingSpaces { get; set; } = default!;
        public DbSet<VehicleType> VehicleTypes { get; set; } = default!;



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
