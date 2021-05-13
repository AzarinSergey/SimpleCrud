using Moedi.Cqrs.Messages;

namespace Person.Domain.Event
{
    public class PersonCreatedDomainEvent : DomainEvent
    {
        public int PersonId { get; set; }
    }
}
