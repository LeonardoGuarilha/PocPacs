using Application.Handlers.QueryHandlers.Studies.OpenStudy;
using Application.ModelBinders;
using Application.Models.Interface;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Mediator;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WadoUriController : ControllerBase
{

    private readonly IMediator _mediator;

    public WadoUriController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Route("wado")]
    [HttpPost]
    public async Task<IActionResult> GetImage([FromBody] OpenStudyInput input)//[ModelBinder(typeof(UriRequest))] IWadoUriRequest request
    {
        // var input = new OpenStudyInput(request.StudyInstanceUid, request.AccessionNumber, request.PatientId, request.OrderId, request.ClinicalTrialSiteID,
        //     request.SeriesInstanceUid, request.SopInstanceUid, request.Frame, request.RequestType, request.ContentType, request.Charset, request.Metadata,
        //     request.Jwt, request.InstanceNumber, request.Lut, request.ImageRequestInfo.Rows, request.ImageRequestInfo.Columns, request.ImageRequestInfo.FrameNumber,
        //     request.ImageRequestInfo.ImageQuality, request.ImageRequestInfo.Thumbnail, request.ImageRequestInfo.Region, request.ImageRequestInfo.WindowCenter,
        //     request.ImageRequestInfo.WindowWidth, request.ImageRequestInfo.PresentationUid, request.ImageRequestInfo.PresentationSeriesUid, request.ImageRequestInfo.TransferSyntax,
        //     request.ImageRequestInfo.Invert, request.ImageRequestInfo.X, request.ImageRequestInfo.Y);

        var output = await _mediator.Send(input);

        return await Task.FromResult(output);
    }
}
