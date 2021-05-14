using AutoMapper;
using Person.Contract;
using Person.Domain.Command;
using Person.Domain.Event;

namespace Person.Implementation
{
    public class MapperConfiguration
    {
        public static void Register(IMapperConfigurationExpression x)
        {
            x.CreateMap<CreatePersonCommandModel, CreatePersonDomainCommand>();
            x.CreateMap<CreatePersonCommandModel, UpdatePersonDomainCommand>();

            x.CreateMap<PersonRemovedDomainEvent, PersonRemovedIntegrationEvent>();
        }
    }
}
