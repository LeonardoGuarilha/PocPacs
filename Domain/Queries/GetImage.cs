using System;

namespace Domain.Queries;

public class GetImage
{
    public string PathName { get; set; }
    public int PathType { get; set; }
    public string SerieDescription { get; set; }
    public string StudyUid { get; set; }
    public string SerieUid { get; set; }
    public string InstanceUid { get; set; }
    public string PatientName { get; set; }
    public string PatientId { get; set; }
    public string StudyDate { get; set; }
    public string AccessionNumber { get; set; }
    public string PatientBirth { get; set; }
    public string Modality { get; set; }
    public string SeriesTime { get; set; }
    public string DiskClientId { get; set; }
    public string DiskSecret { get; set; }
    public string DiskBucket { get; set; }
    public string DiskRegion { get; set; }
    public string FullFileName { get; set; }

    public GetImage()
    {

    }

    public GetImage(string pathName, int pathType, string serieDescription, string studyUid, string serieUid, string instanceUid, string patientName,
        string patientId, string studyDate, string accessionNumber, string patientBirth, string modality, string seriesTime, string diskClientId,
        string diskSecret, string diskBucket, string diskRegion, string fullFileName
    )
    {
        PathName = pathName;
        PathType = pathType;
        SerieDescription = serieDescription;
        StudyUid = studyUid;
        SerieUid = serieUid;
        InstanceUid = instanceUid;
        PatientName = patientName;
        PatientId = patientId;
        StudyDate = studyDate;
        AccessionNumber = accessionNumber;
        PatientBirth = patientBirth;
        Modality = modality;
        SeriesTime = seriesTime;
        DiskClientId = diskClientId;
        DiskSecret = diskSecret;
        DiskBucket = diskBucket;
        DiskRegion = diskRegion;
        FullFileName = fullFileName;
    }
}
