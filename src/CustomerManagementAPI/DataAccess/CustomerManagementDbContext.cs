using CustomerManagementAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementAPI.DataAccess;

public class CustomerManagementDbContext : DbContext
{
    public CustomerManagementDbContext(DbContextOptions<CustomerManagementDbContext> options)
        : base (options) {}

    public DbSet<Customer> Customers => Set<Customer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().HasKey(x => x.CustomerId);
        modelBuilder.Entity<Customer>().ToTable("Customer");

        base.OnModelCreating(modelBuilder);
    }
}