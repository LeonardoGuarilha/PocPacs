using System;
using Application.Common;
using Domain.SearchableRepository;
using MediatR;

namespace Application.Handlers.QueryHandlers.Studies;

public class ListStudiesInput : PaginatedListInput, IRequest<ListStudiesOutput>
{
    public ListStudiesInput(
        int page = 1,
        int perPage = 15,
        string search = "",
        string sort = "",
        SearchOrder dir = SearchOrder.Asc)
         : base(page, perPage, search, sort, dir)
    { }

    // Para não dar problema no binding na controller, no [FromRoute] no método List
    public ListStudiesInput() : base(1, 15, "", "", SearchOrder.Asc)
    { }
}
