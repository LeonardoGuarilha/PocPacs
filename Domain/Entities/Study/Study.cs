using Domain.ValueObjects;
using Shared.Core.DomainObjects;

namespace Domain.Entities.Study;

public class Study : EntityBase, IAggregateRoot
{
    public int IdUnidade { get; set; }
    public StudyUidVO StudyUid { get; set; }
    public string StudyId { get; set; }
    public string AccessionNumber { get; set; }
    public string PatientId { get; set; }
    public string PatientName { get; set; }
    public DateTime StudyDate { get; set; }
    public DateTime StudyTime { get; set; }
    public string StudyDescription { get; set; }
    public string Modality { get; set; }
    public string Series { get; set; }
    public string Status { get; set; }
    public string Messages { get; set; }
    public string PatientSex { get; set; }
    public string PatientBirthDate { get; set; }
    public string RemoteAet { get; set; }
    public string SpecificCharacterSet { get; set; }
    public string NumberOfStudyRelatedSeries { get; set; }
    public string NumberOfStudyRelatedInstances { get; set; }
    public string ReferringPhysiciansName { get; set; }
    public string ModalitiesInStudy { get; set; }

    public Study(int idUnidade, StudyUidVO studyUid, string studyId, string? accessionNumber, string? patientId, string? patientName, DateTime studyDate, DateTime studyTime, string? studyDescription, string? modality, string? series, string? status, string? messages, string? patientSex, string? patientsBirthDate, string? remoteAet, string? specificCharacterSet, string? numberOfStudyRelatedSeries, string? numberOfStudyRelatedInstances, string? modalitiesInStudy, string? referringPhysiciansName)
    {
        IdUnidade = idUnidade;
        StudyUid = studyUid;
        StudyId = studyId;
        AccessionNumber = accessionNumber;
        PatientId = patientId;
        PatientName = patientName;
        StudyDate = studyDate;
        StudyTime = studyTime;
        StudyDescription = studyDescription;
        Modality = modality;
        Series = series;
        Status = status;
        Messages = messages;
        PatientSex = patientSex;
        PatientBirthDate = patientsBirthDate;
        RemoteAet = remoteAet;
        SpecificCharacterSet = specificCharacterSet;
        NumberOfStudyRelatedSeries = numberOfStudyRelatedSeries;
        NumberOfStudyRelatedInstances = numberOfStudyRelatedInstances;
        ModalitiesInStudy = modalitiesInStudy;
        ReferringPhysiciansName = referringPhysiciansName;
    }
}
