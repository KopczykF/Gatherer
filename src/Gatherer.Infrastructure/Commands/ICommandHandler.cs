using System.Threading.Tasks;

namespace Gatherer.Infrastructure.Commands
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
         Task HandleAsync(TCommand command);
    }
}