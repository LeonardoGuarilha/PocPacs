using System;
using Application.Models.Interface;

namespace Application.Models.Impl;

public class WadoUriRequest : IWadoUriRequest
{
    public string RequestType { get; set; }
    public string ContentType { get; set; }
    public string Charset { get; set; }
    public string StudyInstanceUid { get; set; }
    public string SeriesInstanceUid { get; set; }
    public string SopInstanceUid { get; set; }
    public int? Frame { get; set; }
    public bool Anonymize { get; set; }
    public string AccessionNumber { get; set; }
    public string PatientId { get; set; }
    public string OrderId { get; set; }
    public bool Metadata { get; set; }
    public string ClinicalTrialSiteID { get; set; }
    public string Jwt { get; set; }
    public int? InstanceNumber { get; set; }
    public int? Lut { get; set; }

    public IWadoUriImageRequestParams ImageRequestInfo { get; set; }
}
	public class WadoUriImageRequestParams : IWadoUriImageRequestParams
	{
		public int? Rows { get; set; }
		public int? Columns { get; set; }
		public int? FrameNumber { get; set; }
		public int? ImageQuality { get; set; }
		public string Region { get; set; }
		public string WindowCenter { get; set; }
		public string WindowWidth { get; set; }
		public string PresentationUid { get; set; }
		public string PresentationSeriesUid { get; set; }
		public string TransferSyntax { get; set; }
		public bool? Thumbnail { get; set; }
		public bool? Invert { get; set; }
		public int? X { get; set; }
		public int? Y { get; set; }
    }
