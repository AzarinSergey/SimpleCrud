using FluentValidation;
using Person.Domain.Command;


namespace Person.Domain.Validator
{
    public class CreatePersonCommandValidator : AbstractValidator<CreatePersonDomainCommand>
    {
        const string PhonePattern = @"^7\d{10}$";
        const string ZipCodePattern = @"^\d{5}(?:[-\s]\d{4})?$";
        private const string EmailPattern = @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}+$";
        public CreatePersonCommandValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .Matches(PhonePattern)
                .WithMessage(x => $"Phone number format error '{x.PhoneNumber}'. Exspression: '{PhonePattern}'.");

            RuleFor(x => x.ZipCode)
                .Matches(ZipCodePattern)
                .WithMessage(x => $"ZipCode format error '{x.PhoneNumber}'.  Exspression: '{ZipCodePattern}'.");

            RuleFor(x => x.Email)
                .Matches(EmailPattern)
                .WithMessage(x => $"Email format error '{x.PhoneNumber}'.  Exspression: '{EmailPattern}'.");
        }
    }
}
