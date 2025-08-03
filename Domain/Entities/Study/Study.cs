using Domain.ValueObjects;
using Shared.Core.DomainObjects;

namespace Domain.Entities.Study;

public class Study : EntityBase, IAggregateRoot
{
    public int IdUnidade { get; private set; }
    public StudyUidVO StudyUid { get; private set; }
    public string AcNumber { get; private set; }

    public Study(int idUnidade, StudyUidVO studyUid, string acNumber)
    {
        IdUnidade = idUnidade;
        StudyUid = studyUid;
        AcNumber = acNumber;
    }
}
