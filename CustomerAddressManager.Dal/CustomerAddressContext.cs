using CustomerAddressManager.Dal.Models;
using CustomerAddressManager.Dal.Models.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CustomerAddressManager.Dal
{
    public class CustomerAddressContext : DbContext
    {
        public CustomerAddressContext(DbContextOptions<CustomerAddressContext> options) : base(options)
        { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
        }
    }
}
