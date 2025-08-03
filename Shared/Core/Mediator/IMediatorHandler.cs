using Shared.Core.Messaging;

namespace Shared.Core.Mediator;

public interface IMediatorHandler
{
    Task<CommandResult> SendCommand<T>(T command) where T : ICommand;
    Task PublishEvents<T>(T evento) where T : Event;
}
