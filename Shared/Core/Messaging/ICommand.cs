using MediatR;

namespace Shared.Core.Messaging;

public interface ICommand : IRequest<CommandResult>
{
    void Validate();
}
