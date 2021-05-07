using Moedi.Core.Interfaces.Data.Access;
using Moedi.Cqrs.Messages;

namespace Moedi.Cqrs.Processor
{
    public class CommandProcessor<TCommand>
    {
        public CommandProcessor()
        {
            
        }
    }

    public class CommandProcessorBuilder
    {
        private readonly CrossContext _ctx;
        private readonly IUowFactory _uowFactory;

        public CommandProcessorBuilder(CrossContext ctx, IUowFactory uowFactory)
        {
            _ctx = ctx;
            _uowFactory = uowFactory;
        }


    }
}
