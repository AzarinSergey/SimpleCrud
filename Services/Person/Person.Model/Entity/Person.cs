using Moedi.Data.Core.Entity;
using System.ComponentModel.DataAnnotations;

namespace Person.Model.Entity
{
    public class Person : IId
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [MaxLength(128)]
        [Required]
        public string FirstName { get; set; }
        [MaxLength(128)]
        [Required]
        public string LastName { get; set; }
        [MaxLength(128)]
        [Required]
        public string StreetAddress { get; set; }
        [MaxLength(128)]
        [Required]
        public string City { get; set; }
        [MaxLength(64)]
        [Required]
        public string ZipCode { get; set; }
        [MaxLength(32)]
        [Required]
        public string PhoneNumber { get; set; }
        [MaxLength(128)]
        [Required]
        public string Email { get; set; }

    }
}