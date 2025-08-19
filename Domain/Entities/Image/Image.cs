using Shared.Core.DomainObjects;

namespace Domain.Entities.Image;

public class Image : EntityBase, IAggregateRoot
{
    public string SopClassUID { get; set; }
    public string StudyInstanceUid { get; set; }
    public int InstranceNumber { get; set; }
    public string SeriesInstanceUid { get; set; }
    public string ImagePath { get; set; }
    public int NumberOfFrames { get; set; }
    public string Modality { get; set; }
    public int SeriesNumber { get; set; }
    public string SeriesDescription { get; set; }
    public string TransferSyntaxUID { get; set; }
    public string FullFilename { get; set; }
    public int PathType { get; set; }
    public int IdPathName {  get; set; }
    public string AcquisitionNumber { get; set; }
    public string Echonumbers { get; set; }
    public string ImageOrientationPatient { get; set; }
    public string ImagePositionPatient { get; set; }
    public string ImageType { get; set; }
    public int InstanceNumber { get; set; }
    public string SliceLocation { get; set; }
    public string SliceThickness { get; set; }
    public string SopClassUid { get; set; }
    public string SopInstanceUid { get; set; }
    public int ImageRows { get; set; }
    public int ImageColumns { get; set; }

    public Image(string sopClassUID, string studyInstanceUid, int instranceNumber, string seriesInstanceUid, string imagePath, int numberOfFrames, string modality, int seriesNumber, string seriesDescription, string transferSyntaxUID, string fullFilename, int pathType, int idPathName, string acquisitionNumber, string echonumbers, string imageOrientationPatient, string imagePositionPatient, string imageType, int instanceNumber, string sliceLocation, string sliceThickness, string sopClassUid, string sopInstanceUid, int imageRows, int imageColumns)
    {
        SopClassUID = sopClassUID;
        StudyInstanceUid = studyInstanceUid;
        InstranceNumber = instranceNumber;
        SeriesInstanceUid = seriesInstanceUid;
        ImagePath = imagePath;
        NumberOfFrames = numberOfFrames;
        Modality = modality;
        SeriesNumber = seriesNumber;
        SeriesDescription = seriesDescription;
        TransferSyntaxUID = transferSyntaxUID;
        FullFilename = fullFilename;
        PathType = pathType;
        IdPathName = idPathName;
        AcquisitionNumber = acquisitionNumber;
        Echonumbers = echonumbers;
        ImageOrientationPatient = imageOrientationPatient;
        ImagePositionPatient = imagePositionPatient;
        ImageType = imageType;
        InstanceNumber = instanceNumber;
        SliceLocation = sliceLocation;
        SliceThickness = sliceThickness;
        SopClassUid = sopClassUid;
        SopInstanceUid = sopInstanceUid;
        ImageRows = imageRows;
        ImageColumns = imageColumns;
    }
}
