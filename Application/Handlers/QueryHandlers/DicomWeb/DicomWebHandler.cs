using Application.Converters;
using Application.Handlers.QueryHandlers.Studies.OpenStudy;
using Dicom;
using Dicom.Imaging;
using Domain.Dicom;
using Domain.Entities.Image;
using Domain.Repositories.Read;
using Microsoft.AspNetCore.Mvc;

namespace Application.Handlers.QueryHandlers.DicomWeb;

public class DicomWebHandler : IDicomWebHandler
{
    private readonly IDicomWebRepostiroy DicomWebRepostiroy;

    public DicomWebHandler(IDicomWebRepostiroy dicomWebRepostiroy)
    {
        DicomWebRepostiroy = dicomWebRepostiroy;
    }

    public IActionResult RetrieveStudies(List<DicomField> request)
    {
        var dataset = DicomWebRepostiroy.FindStudy(request).ToDataset();

        return new OkObjectResult(dataset);
    }

    public IActionResult RetrieveSeries(List<DicomField> request, string studyInstanceUID)
    {
        request.AddRange(new List<DicomField>
        {
            new DicomField("StudyInstanceUID", studyInstanceUID),
        });

        var dataset = DicomWebRepostiroy.FindSeries(request).ToDataset();

        return new OkObjectResult(dataset);

    }

    public IActionResult GetSeriesThumbnail(string studyInstanceUID, string seriesInstanceUID)
    {
        var parameters = new List<DicomField>
        {
            new DicomField("StudyInstanceUID", studyInstanceUID),
            new DicomField("SeriesInstanceUID", seriesInstanceUID)
        };

        var fullFileName = DicomWebRepostiroy.FindInstancesThumb(parameters).FirstOrDefault().FullFilename;

        var dicomFile = DicomFile.Open(fullFileName);
        var dicomImage = new DicomImage(dicomFile.Dataset);

        using MemoryStream ms = new MemoryStream();
        dicomImage.RenderImage().As<System.Drawing.Bitmap>().Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        ms.Flush();
        ms.Seek(0, SeekOrigin.Begin);

        return new FileStreamResult(ms, MimeMediaTypes.Jpg)
        {
            EnableRangeProcessing = true,
        };
    }

    public IActionResult RetrieveInstances(List<DicomField> request, string studyInstanceUID, string seriesInstanceUID)
    {
        request.AddRange(new List<DicomField>
        {
            new DicomField("StudyInstanceUID", studyInstanceUID),
            new DicomField("SeriesInstanceUID", seriesInstanceUID),
        });

        var dataset = DicomWebRepostiroy.FindInstances(request).ToDataset();

        return new OkObjectResult(dataset);
    }

    public MemoryStream GetInstanceStream(Image instance, string studyInstanceUID, string seriesInstanceUID)
    {
        throw new NotImplementedException();
    }

    public List<Image> FindInstances(List<DicomField> dataset)
    {
        return DicomWebRepostiroy.FindInstances(dataset);
    }
}
