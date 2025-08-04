using System;

namespace Domain.Model;

public class GetStudiesModel
{
    public string? AcNumber { get; set; }
    public string? InitialDate { get; set; }
    public string? FinalDate { get; set; }
    public string? StudyDescription { get; set; }
    public string? PatientId { get; set; }
    public string? Modality { get; set; }
    public string? PatientBirthdate { get; set; }

    public GetStudiesModel(string? acNumber, string? initialDate, string? finalDate, string? studyDescription,
        string? patientId, string? modality, string? patientBirthdate, string? patientName)
    {
        AcNumber = acNumber;
        InitialDate = initialDate;
        FinalDate = finalDate;
        StudyDescription = studyDescription;
        PatientId = patientId;
        Modality = modality;
        PatientBirthdate = patientBirthdate;
        PatientName = patientName;
    }   public string? PatientName { get; set; }

}
