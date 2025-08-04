using System;

namespace Domain.Model;

public class GetStudiesModelOutput
{
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

    public GetStudiesModelOutput(string? studyUid, string? acNumber, string? patientId, string? patientName, string? studyDate, string? studyDescription, string? modality, string? series, string? status, string? messages, string? patientSex, string? patientBirth)
    {
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
