using Gis.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gis.Model.EntityConfig
{
    public class GisDataRequestLogTypeConfiguration : IEntityTypeConfiguration<GisDataRequestLog>
    {
        public void Configure(EntityTypeBuilder<GisDataRequestLog> builder)
        {
            builder.ToTable("GisDataRequestLog");
        }
    }
}