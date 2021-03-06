using Moedi.Cqrs.Messages;

namespace Person.Domain.Command
{
    public class CreatePersonDomainCommand : DomainMessage
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
