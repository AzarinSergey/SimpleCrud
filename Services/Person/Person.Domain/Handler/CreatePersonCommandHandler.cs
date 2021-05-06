using System.Threading;
using System.Threading.Tasks;
using Person.Domain.Command;

namespace Person.Domain.Handler
{
    public class CreatePersonCommandHandler
    {
        public Task Execute(CreatePersonDomainCommand command, CancellationToken token)
        {
            return Task.CompletedTask;
        }
    }
}