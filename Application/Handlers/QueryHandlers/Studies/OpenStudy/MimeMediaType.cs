using System;
using System.Net.Http.Headers;

namespace Application.Handlers.QueryHandlers.Studies.OpenStudy;

public abstract class MimeMediaTypes
{
    private MimeMediaTypes() { }

    public const string Dicom = "application/dicom";
    public const string Pdf = "application/pdf";
    public const string XmlDicom = "application/dicom+xml";
    public const string Json = "application/dicom+json";
    public const string UncompressedData = "application/octet-stream";

    public const string Jpg = "image/jpg";
    public const string Jpeg = "image/jpeg";
    public const string WebP = "image/webp";

    public const string PlainText = "text/plain";
    public const string MultipartRelated = "multipart/related";
    public const string StructuredReport = "text/html";

    public const string Rle = "image/x-dicom-rle";
    public const string Jls = "image/x-jls";
    public const string Jp2 = "image/jp2";
    public const string Jpx = "image/jpx";

    public const string Mpeg2 = "video/mpeg2";
    public const string Mp4 = "video/mp4";

    public static string GetMimeTypeByExtension(string extension)
    {
        return extension.ToLower() switch
        {
            ".pdf" => Pdf,
            ".jpg" => Jpg,
            ".jpeg" => Jpeg,
            ".txt" => PlainText,
            ".mp4" => Mp4,
            ".json" => Json,
            ".dcm" => Dicom,
            _ => "",
        };
    }
}
public class MimeMediaType
{
    public MimeMediaType(string mimeType)
    {
        MimeType = mimeType;
    }

    public override bool Equals(object obj)
    {
        switch (obj)
        {
            case string s:
                return string.Equals(MimeType, s, StringComparison.InvariantCultureIgnoreCase);
            case MimeMediaType type:
                return string.Equals(MimeType, type.MimeType);
            default:
                return false;
        }
    }

    public override int GetHashCode()
    {
        return string.IsNullOrWhiteSpace(MimeType) ? base.GetHashCode() : MimeType.GetHashCode();
    }

    public override string ToString()
    {
        return string.IsNullOrWhiteSpace(MimeType) ? "" : MimeType;
    }
    public string MimeType { get; }

    public static implicit operator string(MimeMediaType mime)
    {
        return mime.MimeType;
    }

    public static implicit operator MimeMediaType(string mime)
    {
        return new MimeMediaType(mime);
    }

    public bool IsIn
    (
        HttpHeaderValueCollection<MediaTypeWithQualityHeaderValue> httpHeaderValueCollection,
        out MediaTypeWithQualityHeaderValue mediaType
    )
    {
        mediaType = httpHeaderValueCollection.FirstOrDefault(n => n.MediaType.Equals(MimeType, StringComparison.InvariantCultureIgnoreCase));

        return mediaType != null;
    }
}
