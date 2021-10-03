using Moedi.Cqrs.Messages;

namespace Gis.Domain.Command
{
    public class CreateAirportsIfNotExistDomainCommand : DomainMessage
    {
        public string[] IataCodes { get; set; }
    }
}
