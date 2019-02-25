using CustomerAddressManager.Dal.Models;
using CustomerAddressManager.Dal.Models.Configurations.Consts;
using FluentValidation;
using System.Linq;

namespace CustomerAddressManager.BusinessDomain.Validators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        private const int MinStringLength = 1;

        public CustomerValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotNull()
                .Length(MinStringLength, FieldConfigurationConsts.IdFieldLength);

            RuleFor(x => x.Name)
                .NotNull()
                .Length(MinStringLength, FieldConfigurationConsts.LongTextFieldLength);

            RuleFor(x => x.Street)
                .Length(MinStringLength, FieldConfigurationConsts.LongTextFieldLength).When(x => x.Street != null);

            RuleFor(x => x.Zip)
                .Length(MinStringLength, FieldConfigurationConsts.ShortTextFieldLength).When(x => x.Zip != null);

            RuleFor(x => x.City)
                .Length(MinStringLength, FieldConfigurationConsts.LongTextFieldLength).When(x => x.City != null);

            RuleFor(x => x.Country)
                .Length(FieldConfigurationConsts.Iso3166_2_CountryCodeLength, FieldConfigurationConsts.Iso3166_2_CountryCodeLength).When(x => x.Country != null);
        }
    }
}
