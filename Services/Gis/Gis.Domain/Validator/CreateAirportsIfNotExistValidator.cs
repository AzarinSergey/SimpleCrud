using System.Linq;
using FluentValidation;
using Gis.Domain.Command;

namespace Gis.Domain.Validator
{
    public class CreateAirportsIfNotExistValidator : AbstractValidator<CreateAirportsIfNotExistDomainCommand>
    {
        public CreateAirportsIfNotExistValidator()
        {
            RuleFor(x => x.IataCodes).Must(x => x.Length > 0 && x.Length <= 10);
            RuleFor(x => x.IataCodes).Must(x => x.All(code => !string.IsNullOrWhiteSpace(code)));
        }
    }
}
