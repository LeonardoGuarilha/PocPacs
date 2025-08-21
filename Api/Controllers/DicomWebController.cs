using Api.MultipartReturn;
using Application.Handlers.QueryHandlers.DicomWeb;
using Application.Handlers.QueryHandlers.Studies.OpenStudy;
using Application.ModelBinders;
using Domain.Dicom;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Result;

namespace Api.Controllers;

[Route("api/dicomweb")]
[ApiController]
public class DicomWebController : ControllerBase
{
    private readonly IDicomWebHandler DicomWebHandler;

    public DicomWebController(IDicomWebHandler dicomWebHandler)
    {
        DicomWebHandler = dicomWebHandler;
    }


    [Route("studies")]
    [HttpGet]
    public IActionResult SearchForStudies([ModelBinder(typeof(DicomWebRequestModelConverter))] List<DicomField> request)
    {
        return DicomWebHandler.RetrieveStudies(request);
    }

    [Route("series")]
    [Route("studies/{studyInstanceUID}/series")]
    [HttpGet]
    public IActionResult SearchForSeries([ModelBinder(typeof(DicomWebRequestModelConverter))] List<DicomField> request, string studyInstanceUid)
    {
        return DicomWebHandler.RetrieveSeries(request, studyInstanceUid);
    }

    [Route("studies/{studyInstanceUID}/series/{seriesInstanceUID}/thumbnail")]
    [HttpGet]
    public IActionResult GetSeriesThumbnail(string studyInstanceUID, string seriesInstanceUID)
    {
        return DicomWebHandler.GetSeriesThumbnail(studyInstanceUID, seriesInstanceUID);
    }

    [Route("instances")]
    [Route("studies/{studyInstanceUID}/instances")]
    [Route("studies/{studyInstanceUID}/series/{seriesInstanceUID}/instances")]
    [HttpGet]
    public IActionResult SearchForInstances([ModelBinder(typeof(DicomWebRequestModelConverter))] List<DicomField> request, string studyInstanceUID, string seriesInstanceUID)
    {
        return DicomWebHandler.RetrieveInstances(request, studyInstanceUID, seriesInstanceUID);
    }
    [Route("studies/{studyInstanceUID}/series/{seriesInstanceUID}/instances/{SOPInstanceUID}")]
    [HttpGet]
    public IActionResult GetInstance(string studyInstanceUID, string seriesInstanceUID, string SOPInstanceUID)
    {
        var parameters = new List<DicomField>
        {
            new DicomField("StudyInstanceUID", studyInstanceUID),
            new DicomField("SeriesInstanceUID", seriesInstanceUID),
            new DicomField("SOPInstanceUID", SOPInstanceUID),
        };

        var instance = DicomWebHandler.FindInstance(parameters);

        if (instance.IsSuccess)
        {
            var response = new MultipartResult("related");

            var file = new StreamReader(instance.Value.FullFilename).BaseStream;

            var content = new MultipartReturn.MultipartContent()
            {
                ContentType = MimeMediaTypes.Dicom,
                FileName = instance.Value.FullFilename,
                Stream = file,
            };

            response.Add(content);

            return response;
        }

        return new BadRequestObjectResult(Error.NoData);
    }
}
