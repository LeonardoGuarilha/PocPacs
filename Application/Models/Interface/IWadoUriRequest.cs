namespace Application.Models.Interface;

public interface IWadoUriRequest
{
    string? StudyInstanceUid { get; set ; }
    string? AccessionNumber { get; set ; }
    string? PatientId { get; set ; }
    string? OrderId { get; set ; }
    string? ClinicalTrialSiteID { get; set ; }
    string? SeriesInstanceUid { get; set ; }
    string? SopInstanceUid {  get; set; }
    int? Frame { get; set; }
    string? RequestType { get; set; }
    string? ContentType { get; set; }
    string? Charset { get; set; }
    bool? Metadata { get; set; }
    IWadoUriImageRequestParams ImageRequestInfo { get; set; }
    string Jwt { get; set; }
    int? InstanceNumber { get; set; }
    int? Lut { get; set; }
}
public interface IWadoUriImageRequestParams
{
    int? Rows { get; set; }
    int? Columns { get; set; }
    int? FrameNumber { get; set; }
    int? ImageQuality { get; set; }
    bool? Thumbnail { get; set; }
    string? Region { get; set; }
    string? WindowCenter { get; set; }
    string? WindowWidth { get; set; }
    string? PresentationUid { get; set; }
    string? PresentationSeriesUid { get; set; }
    string? TransferSyntax { get; set; }
    bool? Invert { get; set; }
    int? X { get; set; }
    int? Y { get; set; }
}