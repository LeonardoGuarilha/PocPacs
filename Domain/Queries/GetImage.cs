using System;

namespace Domain.Queries;

public class GetImage
{
    public string SopClassUID { get; set; }
    public string InstanceUID { get; set; }
    public int InstranceNumber { get; set; }
    public string SeriesUID { get; set; }
    public string ImagePath { get; set; }
    public int NumberOfFrames { get; set; }
    public string Modality { get; set; }
    public int SeriesNumber { get; set; }
    public string SeriesDescription { get; set; }
    public string TransferSyntaxUID { get; set; }
    public int PathType { get; set; }
    public int IdPathName { get; set; }
    public GetImage(string sopClassUID, string instanceUID, int instranceNumber, string seriesUID, string imagePath, int numberOfFrames,
        string modality, int seriesNumber, string seriesDescription, string transferSyntaxUID, int pathType, int idPathName)
    {
        SopClassUID = sopClassUID;
        InstanceUID = instanceUID;
        InstranceNumber = instranceNumber;
        SeriesUID = seriesUID;
        ImagePath = imagePath;
        NumberOfFrames = numberOfFrames;
        Modality = modality;
        SeriesNumber = seriesNumber;
        SeriesDescription = seriesDescription;
        TransferSyntaxUID = transferSyntaxUID;
        PathType = pathType;
        IdPathName = idPathName;
    }
}
