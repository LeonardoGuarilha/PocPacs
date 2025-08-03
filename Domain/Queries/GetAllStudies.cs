using Domain.Entities.Study;
using Shared.Core.Messaging;

namespace Domain.Queries;

public class GetAllStudies : IQuery
{
    public int IdUnidade { get; set; }
    public string StudyUid { get; set; } = string.Empty;
    public string AcNumber { get; set; } = string.Empty;

    public GetAllStudies(int idUnidade, string studyUid, string acNumber)
    {
        IdUnidade = idUnidade;
        StudyUid = studyUid;
        AcNumber = acNumber;
    }

    public static GetAllStudies FromStudies(Study study)
        => new(study.IdUnidade, study.StudyUid.StudyUid, study.AcNumber);
}
