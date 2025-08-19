using Domain.Entities.Study;
using Shared.Core.Messaging;

namespace Domain.Queries;

public class GetAllStudies : IQuery
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
    public string? Status { get; set; }
    public string? Messages { get; set; }
    public string? PatientSex { get; set; }
    public string? PatientBirth { get; set; }

    public GetAllStudies(int idUnidade, string? studyUid, string? acNumber, string? patientId, string? patientName, string? studyDate,
        string? studyDescription, string? modality, string? series, string? status, string? messages, string? patientSex, string? patientBirth)
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

    public GetAllStudies() {}

    public static GetAllStudies FromStudies(Study study)
        => new(study.IdUnidade, study.StudyUid.StudyUid, study.AccessionNumber, study.PatientId, study.PatientName, study.StudyDate.ToString(), study.StudyDescription,
            study.Modality, study.Series, study.Status, study.Messages, study.PatientSex, study.PatientBirthDate);
}

