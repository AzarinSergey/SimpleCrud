using FluentValidation;
using Person.Domain.Command;


namespace Person.Domain.Validator
{
    public class CreatePersonCommandValidator : AbstractValidator<CreatePersonDomainCommand>
    {
        public CreatePersonCommandValidator()
        {
            PersonValidations.ValidatePhoneNumber(RuleFor(x => x.PhoneNumber));
            PersonValidations.ValidateZipCode(RuleFor(x => x.ZipCode));
            PersonValidations.ValidateEmail(RuleFor(x => x.Email));
        }
    }
}
