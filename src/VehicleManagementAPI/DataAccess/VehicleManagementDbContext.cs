using Microsoft.EntityFrameworkCore;
using VehicleManagementAPI.Model;

namespace VehicleManagementAPI.DataAccess;

public class VehicleManagementDbContext : DbContext
{
    public VehicleManagementDbContext(DbContextOptions<VehicleManagementDbContext> options)
        : base(options) {}

    public DbSet<Vehicle> Vehicles => Set<Vehicle>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vehicle>().HasKey(x => x.LicenseNumber);
        modelBuilder.Entity<Vehicle>().ToTable("Vehicle");
        
        base.OnModelCreating(modelBuilder);
    }
}