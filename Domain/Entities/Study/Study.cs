using Domain.ValueObjects;
using Shared.Core.DomainObjects;

namespace Domain.Entities.Study;

public class Study : EntityBase, IAggregateRoot
{
    public int IdUnidade { get; set; }
    public StudyUidVO StudyUid { get; set; }
    public string? AcNumber { get; set; }
    public string? PatientId { get; set; }
    public string? PatientName { get; set; }
    public string? StudyDate { get; set; }
    public string? StudyDescription { get; set; }
    public string? Modality { get; set; }
    public string? Series { get; set; }
    public string? Status { get; set; }
    public string? Messages { get; set; }
    public string? PatientSex { get; set; }
    public string? PatientBirth { get; set; }

    public Study(int idUnidade, StudyUidVO studyUid, string? acNumber, string? patientId, string? patientName, string? studyDate, string? studyDescription, string? modality, string? series, string? status, string? messages, string? patientSex, string? patientBirth)
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
        Status = status;
        Messages = messages;
        PatientSex = patientSex;
        PatientBirth = patientBirth;
    }

}
