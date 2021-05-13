using Moedi.Cqrs.Messages;

namespace Person.Domain.Command
{
    public class RemovePersonDomainCommand : DomainMessage
    {
        public int Id { get; set; }
    }
}