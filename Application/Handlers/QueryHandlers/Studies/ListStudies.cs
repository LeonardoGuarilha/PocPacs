
using Domain.Queries;
using Domain.Repositories.Read;

namespace Application.Handlers.QueryHandlers.Studies;

public class ListStudies : IListStudies
{
    private readonly IStudyRepository _studyRepository;

    public ListStudies(IStudyRepository studyRepository)
    {
        _studyRepository = studyRepository;
    }

    public async Task<ListStudiesOutput> Handle(ListStudiesInput request, CancellationToken cancellationToken)
    {
        var studies = await _studyRepository.Search(
            new(request.Page, request.PerPage, request.Search, request.Sort, request.Dir),
            cancellationToken
        );

        return new ListStudiesOutput(
            studies.CurrentPage,
            studies.PerPage,
            studies.Total,
            studies.Items
                .Select(x => GetAllStudies.FromStudies(x))
                .ToList()
        );
    }
}
