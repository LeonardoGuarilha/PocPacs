using MediatR;
using Shared.Core.Messaging;

namespace Application.Handlers.QueryHandlers.Studies;

public interface IListStudies : IRequestHandler<ListStudiesInput, CommandResult>
{

}
