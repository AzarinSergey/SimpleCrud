using AutoMapper;
using Projection.Contract.Models;
using Projection.Domain.Model;

namespace Projection.Implementation.Monolithic
{
    public class MapperConfiguration
    {
        public static void Register(IMapperConfigurationExpression x)
        {
            x.CreateMap<SearchPersonFilterPrj, SearchPersonFilter>();
            x.CreateMap<SearchPersonResultItem, SearchPersonResultPrj>();
        }
    }
}
