using System;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace Api.MultipartReturn;

public class MultipartResult : Collection<MultipartContent>, IActionResult
{
    private readonly System.Net.Http.MultipartContent content;

    public MultipartResult(string subtype = "byteranges", string boundary = null)
    {
        if (boundary == null)
        {
            this.content = new System.Net.Http.MultipartContent(subtype);
        }
        else
        {
            this.content = new System.Net.Http.MultipartContent(subtype, boundary);
        }
    }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        Parallel.ForEach(this, item =>
        {

            if (item.Stream != null)
            {
                var content = new StreamContent(item.Stream);

                if (item.ContentType != null)
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue(item.ContentType);
                }

                if (item.FileName != null)
                {
                    var contentDisposition = new ContentDispositionHeaderValue("attachment");
                    content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    content.Headers.ContentDisposition.FileName = contentDisposition.FileName;
                    content.Headers.ContentDisposition.FileNameStar = contentDisposition.FileNameStar;
                }

                this.content.Add(content);
            }
        });

        context.HttpContext.Response.ContentLength = content.Headers.ContentLength;
        context.HttpContext.Response.ContentType = content.Headers.ContentType.ToString();

        await content.CopyToAsync(context.HttpContext.Response.Body);
    }
}
