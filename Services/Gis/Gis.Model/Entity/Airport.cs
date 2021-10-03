using Moedi.Data.Core.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gis.Model.Entity
{
    public class Airport : IId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public string Name { get; set; }
        public string Iata { get; set; }

        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}