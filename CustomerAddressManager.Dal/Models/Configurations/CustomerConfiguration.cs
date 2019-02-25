using CustomerAddressManager.Dal.Models.Configurations.Consts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerAddressManager.Dal.Models.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(x => new { x.CustomerId, x.Name });

            builder.Property(x => x.CustomerId)
                .IsUnicode(false)
                .HasMaxLength(FieldConfigurationConsts.IdFieldLength)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(FieldConfigurationConsts.LongTextFieldLength)
                .IsRequired();

            builder.Property(x => x.Street)
                .HasMaxLength(FieldConfigurationConsts.LongTextFieldLength);

            builder.Property(x => x.Zip)
                .IsUnicode(false)
                .HasMaxLength(FieldConfigurationConsts.ShortTextFieldLength);

            builder.Property(x => x.City)
                .HasMaxLength(FieldConfigurationConsts.LongTextFieldLength);

            builder.Property(x => x.Country)
                .IsUnicode(false)
                .HasMaxLength(FieldConfigurationConsts.Iso3166_2_CountryCodeLength);
        }
    }
}
