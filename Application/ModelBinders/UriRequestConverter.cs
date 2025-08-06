using System;
using Application.Models.Impl;
using Application.Models.Interface;
using Microsoft.AspNetCore.Http;

namespace Application.ModelBinders;

public class UriRequestConverter
{
    public bool TryParse(HttpRequest request, out IWadoUriRequest result)
    {
        var wadoReq = new WadoUriRequest();

        if (request.Query.Keys.Count > 0)
        {
            var query = request.Query;

            wadoReq.RequestType = query[WadoRequestKeys.RequestType];
            wadoReq.StudyInstanceUid = query[WadoRequestKeys.StudyUid];
            wadoReq.SeriesInstanceUid = query[WadoRequestKeys.SeriesUid];
            wadoReq.SopInstanceUid = query[WadoRequestKeys.ObjectUid];
            wadoReq.ClinicalTrialSiteID = query[WadoRequestKeys.ClinicalTrialSiteId];
            wadoReq.ContentType = query[WadoRequestKeys.ContentType];
            wadoReq.Charset = query[WadoRequestKeys.Charset];
            wadoReq.Jwt = query[WadoRequestKeys.Jwt];
            wadoReq.InstanceNumber = GetIntValue(query[WadoRequestKeys.InstanceNumber]);
            wadoReq.Lut = GetIntValue(query[WadoRequestKeys.Lut]);

            wadoReq.Anonymize = string.Compare(query[WadoRequestKeys.Anonymize], "yes", true) == 0;
            wadoReq.Metadata = string.Compare(query["metadata"], "true", true) == 0;

            wadoReq.ImageRequestInfo = new WadoUriImageRequestParams
            {
                Rows = GetIntValue(query[WadoRequestKeys.Rows]),
                Columns = GetIntValue(query[WadoRequestKeys.Columns]),
                Region = query[WadoRequestKeys.Region],
                WindowWidth = query[WadoRequestKeys.WindowWidth],
                WindowCenter = query[WadoRequestKeys.WindowCenter]
            };

            wadoReq.Frame = wadoReq.ImageRequestInfo.FrameNumber = GetIntValue(query[WadoRequestKeys.FrameNumber]);
            wadoReq.ImageRequestInfo.ImageQuality = GetIntValue(query[WadoRequestKeys.ImageQuality]);
            wadoReq.ImageRequestInfo.PresentationUid = query[WadoRequestKeys.PresentationUid];
            wadoReq.ImageRequestInfo.PresentationSeriesUid = query[WadoRequestKeys.PresentationSeriesUid];
            wadoReq.ImageRequestInfo.TransferSyntax = query[WadoRequestKeys.TransferSyntax];
            wadoReq.ImageRequestInfo.Invert = string.Compare(query["invert"], "true", true) == 0;
            wadoReq.ImageRequestInfo.Thumbnail = string.Compare(query[WadoRequestKeys.Thumbnail], "true", true) == 0;
            wadoReq.ImageRequestInfo.X = GetIntValue(query["X"]);
            wadoReq.ImageRequestInfo.Y = GetIntValue(query["Y"]);
        }

        result = wadoReq;

        return true;
    }
    private static int? GetIntValue(string stringValue)
    {
        if (string.IsNullOrWhiteSpace(stringValue))
        {
            return null;
        }

        if (int.TryParse(stringValue.Trim(), out var parsedVal))
        {
            return parsedVal;
        }
        return null;
    }
}
public abstract class WadoRequestKeys
{
    private WadoRequestKeys() { }

    public const string RequestType = "requestType";
    public const string StudyUid = "studyUID";
    public const string SeriesUid = "seriesUID";
    public const string ObjectUid = "objectUID";
    public const string ClinicalTrialSiteId = "clinicalTrialSiteID";
    public const string ContentType = "contentType";
    public const string Charset = "charset";
    public const string Anonymize = "anonymize";
    public const string Rows = "rows";
    public const string Columns = "columns";
    public const string Region = "region";
    public const string WindowWidth = "windowWidth";
    public const string WindowCenter = "windowCenter";
    public const string FrameNumber = "frame";
    public const string ImageQuality = "imageQuality";
    public const string PresentationUid = "presentationUID";
    public const string PresentationSeriesUid = "presentationSeriesUID";
    public const string TransferSyntax = "transferSyntax";
    public const string Thumbnail = "thumbnail";
    public const string Jwt = "jwt";
    public const string InstanceNumber = "instanceNumber";
    public const string Lut = "lut";
}