using System;

namespace Api.MultipartReturn;

public class MultipartContent
{
    public string ContentType { get; set; }

    public string FileName { get; set; }

    public Stream Stream { get; set; }
}
