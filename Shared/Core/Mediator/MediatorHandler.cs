using MediatR;
using Shared.Core.Messaging;

namespace Shared.Core.Mediator;

public class MediatorHandler : IMediatorHandler
{
    private readonly IMediator _mediator;

    public MediatorHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task PublishEvents<T>(T evento) where T : Event
    {
        await _mediator.Publish(evento);
    }

    public async Task<CommandResult> SendCommand<T>(T command) where T : ICommand
    {
        return await _mediator.Send(command);
    }
}
