using MediatR;

namespace Application.Handlers.QueryHandlers.Studies;

public interface IListStudies : IRequestHandler<ListStudiesInput, ListStudiesOutput>
{

}
