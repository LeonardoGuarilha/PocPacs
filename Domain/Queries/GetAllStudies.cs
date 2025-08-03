using Shared.Core.Messaging;

namespace Domain.Queries;

public class GetAllStudies : IQuery
{
    public int IdUnidade { get; set; }
    public string StudyUid { get; set; } = string.Empty;
    public string AcNumber { get; set; } = string.Empty;
}
