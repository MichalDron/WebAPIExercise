using CustomerAddressManager.Dal.Models.Configurations.Consts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerAddressManager.Dal.Models.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");

            builder.HasKey(x => new { x.CustomerId, x.AddressType });

            builder.Property(x => x.CustomerId)
                .IsUnicode(false)
                .HasMaxLength(FieldConfigurationConsts.IdFieldLength)
                .IsRequired();

            builder.Property(x => x.AddressType)
                .IsUnicode(false)
                .HasMaxLength(FieldConfigurationConsts.TypeFieldLength)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(FieldConfigurationConsts.LongTextFieldLength);

            builder.Property(x => x.Street)
                .HasMaxLength(FieldConfigurationConsts.LongTextFieldLength);

            builder.Property(x => x.Zip)
                .IsUnicode(false)
                .HasMaxLength(FieldConfigurationConsts.ShortTextFieldLength);

            builder.Property(x => x.City)
                .IsUnicode()
                .HasMaxLength(FieldConfigurationConsts.LongTextFieldLength);

            builder.Property(x => x.Country)
                .IsUnicode(false)
                .HasMaxLength(FieldConfigurationConsts.Iso3166_2_CountryCodeLength);
        }
    }
}
