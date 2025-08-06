using MediatR;
using Shared.Core.Messaging;

namespace Application.Handlers.QueryHandlers.Studies.ListStudies;

public interface IListStudies : IRequestHandler<ListStudiesInput, CommandResult>
{

}
