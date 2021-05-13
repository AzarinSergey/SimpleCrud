using FluentValidation;

namespace Person.Domain.Validator
{
    public static class PersonValidations
    {
        private const string PhonePattern = @"^7\d{10}$";
        private const string ZipCodePattern = @"^\d{5}(?:[-\s]\d{4})?$";
        private const string EmailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

        public static void ValidatePhoneNumber<T>(IRuleBuilderInitial<T, string> ruleBuilder)
        {
            ruleBuilder.Matches(PhonePattern)
                .WithMessage(x => $"Phone number format error. Exspression: '{PhonePattern}'.");
        }

        public static void ValidateZipCode<T>(IRuleBuilderInitial<T, string> ruleBuilder)
        {
            ruleBuilder
                .Matches(ZipCodePattern)
                .WithMessage(x => $"ZipCode format error'.  Exspression: '{ZipCodePattern}'.");
        }

        public static void ValidateEmail<T>(IRuleBuilderInitial<T, string> ruleBuilder)
        {
            ruleBuilder
                .Matches(EmailPattern)
                .WithMessage(x => $"Email format error.  Exspression: '{EmailPattern}'.");
        }
    }
}