using Shared.Core.Messaging;

namespace Domain.Queries;

public class GetMetadata : IQuery
{
    public int? IdUnidade { get; set; }
    public string? StudyUid { get; set; }
    public string? AcNumber { get; set; }
    public string? PatientId { get; set; }
    public string? PatientName { get; set; }
    public string? StudyDate { get; set; }
    public string? StudyDescription { get; set; }
    public string? Modality { get; set; }
    public string? Series { get; set; }
    public string? PatientSex { get; set; }
    public string? PatientBirth { get; set; }

    public GetMetadata(int? idUnidade, string? studyUid, string? acNumber, string? patientId, string? patientName, string? studyDate,
         string? studyDescription, string? modality, string? series, string? patientSex, string? patientBirth)
    {
        IdUnidade = idUnidade;
        StudyUid = studyUid;
        AcNumber = acNumber;
        PatientId = patientId;
        PatientName = patientName;
        StudyDate = studyDate;
        StudyDescription = studyDescription;
        Modality = modality;
        Series = series;
        PatientSex = patientSex;
        PatientBirth = patientBirth;
    }
}
