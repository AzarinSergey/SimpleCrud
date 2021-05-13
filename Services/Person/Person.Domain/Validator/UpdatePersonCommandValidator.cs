using FluentValidation;
using Person.Domain.Command;

namespace Person.Domain.Validator
{
    public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonDomainCommand>
    {
        public UpdatePersonCommandValidator()
        {
            PersonValidations.ValidatePhoneNumber(RuleFor(x => x.PhoneNumber));
            PersonValidations.ValidateZipCode(RuleFor(x => x.ZipCode));
            PersonValidations.ValidateEmail(RuleFor(x => x.Email));
        }
    }
}