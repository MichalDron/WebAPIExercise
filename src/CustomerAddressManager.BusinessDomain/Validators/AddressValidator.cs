using CustomerAddressManager.Dal.Models;
using CustomerAddressManager.Dal.Models.Configurations.Consts;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace CustomerAddressManager.BusinessDomain.Validators
{
    public class AddressValidator : AbstractValidator<Address>
    {
        private class AddressType
        {
            public const string InvoiceType = "I";
            public const string DeliveryType = "D";
            public const string ServiceType = "S";
        }

        private static readonly List<string> EnableAddressTypes = new List<string> {
            AddressType.InvoiceType,
            AddressType.DeliveryType,
            AddressType.ServiceType
        };

        public static int EnableAddressTypesCount => EnableAddressTypes.Count();

        private const int MinStringLength = 1;

        public AddressValidator(bool customerIdNotRequired = false)
        {
            RuleFor(x => x.CustomerId)
                .NotNull().When(x => !customerIdNotRequired)
                .Length(MinStringLength, FieldConfigurationConsts.IdFieldLength);

            RuleFor(x => x.AddressType)
                .NotNull()
                .Length(FieldConfigurationConsts.TypeFieldLength, FieldConfigurationConsts.TypeFieldLength)
                .Must(x => EnableAddressTypes.Contains(x))
                .WithMessage($"Address type must be one of {{{string.Join(",", EnableAddressTypes)}}}");

            RuleFor(x => x.Name)
                .Length(MinStringLength, FieldConfigurationConsts.LongTextFieldLength).When(x => x.Name != null);

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
