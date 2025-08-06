using Application.Handlers.QueryHandlers.Studies.OpenStudy;
using Application.ModelBinders;
using Application.Models.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WadoUriController : ControllerBase
{
    [Route("wado")]
    public IActionResult GetImage([ModelBinder(typeof(UriRequest))]IWadoUriRequest request)
    {
        if (request.Metadata)
        {
            // Retornar o metadata
        }
        else if (request.ImageRequestInfo.Thumbnail != null && request.ImageRequestInfo.Thumbnail.Value)
        {
            // Retornar a thumbnail
        }

        switch (request.ContentType)
        {
            case MimeMediaTypes.Dicom:
            // Retornar Dicom
            case MimeMediaTypes.Jpg:
            // Retornar Jpg
            default:
            // Retornar Dicom
            return Ok("");
        }
    }
}
