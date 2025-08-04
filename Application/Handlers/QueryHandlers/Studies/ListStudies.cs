using Domain.Model;
using Domain.Queries;
using Domain.Repositories.Read;
using Domain.SearchableRepository;
using Shared.Core.Messaging;

namespace Application.Handlers.QueryHandlers.Studies;

public class ListStudies : IListStudies
{
    private readonly IStudyRepository _studyRepository;

    public ListStudies(IStudyRepository studyRepository)
    {
        _studyRepository = studyRepository;
    }

    public async Task<CommandResult> Handle(ListStudiesInput request, CancellationToken cancellationToken)
    {
        request.Validate();

        var data = new GetStudiesModel(request.AcNumber, request.InitialDate, request.FinalDate, request.StudyDescription, request.PatientName,
            request.Modality, request.PatientBirthdate, request.PatientName);

        var studies = await _studyRepository.GetAllStudies(data, 1);

        if (studies.IsSuccess)
        {
            var query = AddOrdererToQuery(studies.Value.AsQueryable()!, request.Sort, request.Dir);
            var studiesFiltered = query.Skip((request.Page - 1) * request.PerPage).Take(request.PerPage).ToList();

            var output = new ListStudiesOutput(
                request.Page,
                request.PerPage,
                studies.Value.Count,
                studiesFiltered
            );

            return new CommandResult(
                true,
                "Estudos encontrados!",
                output
            );
        }

        return new CommandResult(
            false,
            "Erro ao rodar a query para buscar os estudos",
            null
        );
    }

    private IQueryable<GetAllStudies> AddOrdererToQuery(
        IQueryable<GetAllStudies> query,
        string orderProperty,
        SearchOrder order
    )
    {
        var orderedQuery = (orderProperty.ToLower(), order) switch
        {
            ("acnumber", SearchOrder.Asc) => query.OrderBy(x => x.AcNumber),
            ("acnumber", SearchOrder.Desc) => query.OrderByDescending(x => x.AcNumber),
            ("patientid", SearchOrder.Asc) => query.OrderBy(x => x.PatientId),
            ("patientid", SearchOrder.Desc) => query.OrderByDescending(x => x.PatientId),
            ("patientname", SearchOrder.Asc) => query.OrderBy(x => x.PatientName),
            ("patientname", SearchOrder.Desc) => query.OrderByDescending(x => x.PatientName),
            ("studydate", SearchOrder.Asc) => query.OrderBy(x => x.StudyDate),
            ("studydate", SearchOrder.Desc) => query.OrderByDescending(x => x.StudyDate),
            ("series", SearchOrder.Asc) => query.OrderBy(x => x.Series),
            ("series", SearchOrder.Desc) => query.OrderByDescending(x => x.Series),
            ("modality", SearchOrder.Asc) => query.OrderBy(x => x.Modality),
            ("modality", SearchOrder.Desc) => query.OrderByDescending(x => x.Modality),
            _ => query.OrderBy(x => x.PatientName)
        };

        return orderedQuery;
    }
}
