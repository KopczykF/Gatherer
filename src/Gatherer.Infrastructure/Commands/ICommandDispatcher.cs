using System.Threading.Tasks;

namespace Gatherer.Infrastructure.Commands
{
    public interface ICommandDispatcher
    {
         Task DispatchAsync<TCommand>(TCommand command) where TCommand : ICommand;
    }
}