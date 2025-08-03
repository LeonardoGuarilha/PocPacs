using MediatR;

namespace Shared.Core.Messaging;

public interface IQuery : IRequest<CommandResult>
{

}
