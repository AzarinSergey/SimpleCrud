using Moedi.Cqrs.Messages;

namespace Person.Domain.Event
{
    public class PersonRemovedDomainEvent : DomainEvent
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}