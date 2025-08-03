using Application.Common;
using Domain.Queries;

namespace Application.Handlers.QueryHandlers.Studies;

public class ListStudiesOutput : PaginatedListOutput<GetAllStudies>
{
    public ListStudiesOutput(int page, int perPage, int total, IReadOnlyList<GetAllStudies> items)
        : base(page, perPage, total, items)
    {
    }
}
