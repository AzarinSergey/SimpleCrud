using System.ComponentModel.DataAnnotations;
using Moedi.Data.Core.Entity;

namespace Gis.Model.Entity
{
    public class GisDataRequestLog : IId
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public bool Success { get; set; }
    }
}