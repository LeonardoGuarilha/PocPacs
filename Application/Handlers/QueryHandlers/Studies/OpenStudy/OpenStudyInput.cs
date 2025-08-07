using Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Messaging;

namespace Application.Handlers.QueryHandlers.Studies.OpenStudy;

public class OpenStudyInput :ICommand, IRequest<IActionResult>
{
    public string StudyInstanceUid { get; set; } = string.Empty;
    public string AccessionNumber { get; set; } = string.Empty;
    public string PatientId { get; set; } = string.Empty;
    public string OrderId { get; set; } = string.Empty;
    public string ClinicalTrialSiteID { get; set; } = string.Empty;
    public string SeriesInstanceUid { get; set; } = string.Empty;
    public string SopInstanceUid { get; set; } = string.Empty;
    public int? Frame { get; set; }
    public string RequestType { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public string Charset { get; set; } = string.Empty;
    public bool Metadata { get; set; }
    public string Jwt { get; set; } = string.Empty;
    public int? InstanceNumber { get; set; }
    public int? Lut { get; set; }
    public int? Rows { get; set; }
    public int? Columns { get; set; }
    public int? FrameNumber { get; set; }
    public int? ImageQuality { get; set; }
    public bool? Thumbnail { get; set; }
    public string Region { get; set; } = string.Empty;
    public string WindowCenter { get; set; } = string.Empty;
    public string WindowWidth { get; set; } = string.Empty;
    public string PresentationUid { get; set; } = string.Empty;
    public string PresentationSeriesUid { get; set; } = string.Empty;
    public string TransferSyntax { get; set; } = string.Empty;
    public bool? Invert { get; set; }
    public int? X { get; set; }
    public int? Y { get; set; }

    public OpenStudyInput() { }

    public OpenStudyInput(string studyInstanceUid, string accessionNumber, string patientId, string orderId,
        string clinicalTrialSiteID, string seriesInstanceUid, string sopInstanceUid, int? frame, string requestType,
        string contentType, string charset, bool metadata, string jwt, int? instanceNumber, int? lut, int? rows,
        int? columns, int? frameNumber, int? imageQuality, bool? thumbnail, string region, string windowCenter,
        string windowWidth, string presentationUid, string presentationSeriesUid, string transferSyntax,
        bool? invert, int? x, int? y)
    {
        StudyInstanceUid = studyInstanceUid;
        AccessionNumber = accessionNumber;
        PatientId = patientId;
        OrderId = orderId;
        ClinicalTrialSiteID = clinicalTrialSiteID;
        SeriesInstanceUid = seriesInstanceUid;
        SopInstanceUid = sopInstanceUid;
        Frame = frame;
        RequestType = requestType;
        ContentType = contentType;
        Charset = charset;
        Metadata = metadata;
        Jwt = jwt;
        InstanceNumber = instanceNumber;
        Lut = lut;
        Rows = rows;
        Columns = columns;
        FrameNumber = frameNumber;
        ImageQuality = imageQuality;
        Thumbnail = thumbnail;
        Region = region;
        WindowCenter = windowCenter;
        WindowWidth = windowWidth;
        PresentationUid = presentationUid;
        PresentationSeriesUid = presentationSeriesUid;
        TransferSyntax = transferSyntax;
        Invert = invert;
        X = x;
        Y = y;
    }

    public void Validate()
    {
        throw new NotImplementedException();
    }
}
