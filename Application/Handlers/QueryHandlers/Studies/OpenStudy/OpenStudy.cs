using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using Dicom;
using Dicom.Imaging;
using Dicom.Imaging.LUT;
using Domain.Repositories.Read;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Result;

namespace Application.Handlers.QueryHandlers.Studies.OpenStudy;

public class OpenStudy : IOpenStudy
{
    private const string LUT_SIGMOID = "SIGMOID";
    private const string LUT_LINEAR = "LINEAR";
    private const string LUT_LINEAR_EXACT = "LINEAR_EXACT";
    private readonly IWadoRepository _wadoRepository;

    public OpenStudy(IWadoRepository wadoRepository)
    {
        _wadoRepository = wadoRepository;
    }

    public async Task<IActionResult> Handle(OpenStudyInput request, CancellationToken cancellationToken)
    {
        ImageManager.SetImplementation(WinFormsImageManager.Instance);

        if (request.Metadata)
        {
            var data = await _wadoRepository.RetrieveMetadata(request.StudyInstanceUid, request.SeriesInstanceUid, request.SopInstanceUid, int.Parse(request.ClinicalTrialSiteID));

            if (data.IsSuccess)
            {

            }
        }
        else if (request.Thumbnail != null && request.Thumbnail.Value)
        {
            var data = await _wadoRepository.RetrieveSopInstance(request.StudyInstanceUid, request.SeriesInstanceUid, request.SopInstanceUid, int.Parse(request.ClinicalTrialSiteID));

            if (data.IsSuccess)
            {

            }
        }

        switch (request.ContentType)
        {
            case MimeMediaTypes.Dicom:
            // Retornar Dicom
            case MimeMediaTypes.Jpg:
            // Retornar Jpg
            default:
                // Retornar Dicom
                return new EmptyResult();
        }
    }

    private async Task<IActionResult> GetInstance(OpenStudyInput request)
    {
        var imageFrame = request.FrameNumber ?? 0;
        var image = await LoadImage(request);

        Stream resultValue = new MemoryStream();
        using (var bmp = image.Value.RenderImage(imageFrame))
        {
            var rendered = bmp.As<Bitmap>();

            rendered.Save(resultValue, ImageFormat.Jpeg);
        }

        resultValue.Flush();
        resultValue.Seek(0, SeekOrigin.Begin);

        return new FileStreamResult(resultValue, MimeMediaTypes.Jpg)
        {
            EnableRangeProcessing = true
        };
    }

    private async Task<Result<DicomImage>> LoadImage(OpenStudyInput request)
    {
        Stopwatch watch = new Stopwatch();
        watch.Start();

        var data = await _wadoRepository.RetrieveSopInstance(request.StudyInstanceUid, request.SeriesInstanceUid, request.SopInstanceUid, int.Parse(request.ClinicalTrialSiteID));

        if (data.IsSuccess)
        {
            var file = DicomFile.Open(data.Value.ImagePath);

            watch.Reset();
            watch.Restart();

            var image = new DicomImage(file.Dataset);
            if (request.Lut.HasValue)
            {
                image = ApplyLut(data.Value.ImagePath, image, request.Lut ?? 0, file).Value;
            }

            return Result.Success<DicomImage>(image);
        }

        return Result.Failure<DicomImage>(Error.NullValue);
    }

    private Result<DicomImage> ApplyLut(string filePath, DicomImage image, int lut = -1, DicomFile? dicomFile = null)
    {
        dicomFile = dicomFile ?? DicomFile.Open(filePath);
        var dataset = dicomFile.Dataset;
        image = new DicomImage(dataset);

        if (lut == 0)
        {
            dicomFile.Dataset.AddOrUpdate(DicomTag.VOILUTFunction, LUT_LINEAR);
            image = new DicomImage(dicomFile.Dataset);
            var opts = GrayscaleRenderOptions.FromDataset(dicomFile.Dataset);
            var linearLut = VOILUT.Create(opts);
            linearLut.Recalculate();
        }
        else if (lut == 1)
        {
            dicomFile.Dataset.AddOrUpdate(DicomTag.VOILUTFunction, LUT_SIGMOID);
            image = new DicomImage(dicomFile.Dataset);
            var opts = GrayscaleRenderOptions.FromDataset(dicomFile.Dataset);
            var lutSigmoid = VOILUT.Create(opts);
            lutSigmoid.Recalculate();

        }
        else if (lut == 2)
        {
            dicomFile.Dataset.AddOrUpdate(DicomTag.VOILUTFunction, LUT_LINEAR_EXACT);
            image = new DicomImage(dicomFile.Dataset);
            var opts = GrayscaleRenderOptions.FromDataset(dicomFile.Dataset);
            var lutLinearExact = VOILUT.Create(opts);
            lutLinearExact.Recalculate();
        }

        return image;
    }
}
