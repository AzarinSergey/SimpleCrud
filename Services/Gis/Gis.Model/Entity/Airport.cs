using Moedi.Data.Core.Entity;
using System.ComponentModel.DataAnnotations;

namespace Gis.Model.Entity
{
    public class Airport : IId
    {
        [Key]
        [Required]
        public int Id { get; set; }
    }
}