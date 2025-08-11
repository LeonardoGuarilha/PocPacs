using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using System.Text.Json;
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
                MemoryStream memoryStream = new MemoryStream(Encoding.Default.GetBytes(JsonSerializer.Serialize(data.Value)));
                memoryStream.Flush();
                memoryStream.Seek(0, SeekOrigin.Begin);

                return new FileStreamResult(memoryStream, MimeMediaTypes.Json)
                {
                    EnableRangeProcessing = true
                };
            }
        }
        else if (request.Thumbnail != null && request.Thumbnail.Value)
        {
            var data = await _wadoRepository.RetrieveSopInstance(request.StudyInstanceUid, request.SeriesInstanceUid, request.SopInstanceUid, int.Parse(request.ClinicalTrialSiteID));

            if (data.IsSuccess)
            {
                var imageThumb = await LoadImage(request);

                if (imageThumb.IsSuccess)
                {
                    Stream returnValue3 = new MemoryStream();

                    using (var bmp = imageThumb.Value.RenderImage())
                    {
                        var rendered = bmp.AsClonedBitmap();
                        Resize(rendered, 16, 16).Save(returnValue3, ImageFormat.Jpeg);
                    }

                    returnValue3.Flush();
                    returnValue3.Seek(0, SeekOrigin.Begin);

                    return new FileStreamResult(returnValue3, MimeMediaTypes.Jpg)
                    {
                        EnableRangeProcessing = true
                    };
                }
            }
        }

        return request.ContentType switch
        {
            MimeMediaTypes.Dicom => await GetDicom(request),
            MimeMediaTypes.Jpg => await GetInstance(request),
            _ => await GetInstance(request),
        };
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

    private async Task<IActionResult> GetDicom(OpenStudyInput request)
    {
        var data = await _wadoRepository.RetrieveSopInstance(request.StudyInstanceUid, request.SeriesInstanceUid, request.SopInstanceUid, int.Parse(request.ClinicalTrialSiteID));
        Stream file = File.OpenRead(data.Value.ImagePath);

        return new FileStreamResult(file, MimeMediaTypes.Dicom)
        {
            EnableRangeProcessing = true
        };
    }

    private Bitmap Resize(Bitmap file, int w = 211, int h = 131, bool maintainSize = false)
    {
        if (maintainSize)
        {
            return ResizeImage(file, new Size((int)(file.Width * .5), (int)(file.Height * .5)));
        }
        else
        {
            var scale = Math.Min(w * 8f / file.Width, h * 8f / file.Height);
            return ResizeImage(file, new Size((int)(file.Width * scale), (int)(file.Height * scale)));
        }
    }

    public Bitmap ResizeImage(Image imgToResize, Size size)
    {
        var sourceWidth = imgToResize.Width;
        var sourceHeight = imgToResize.Height;

        var nPercentW = (size.Width / (float)sourceWidth);
        var nPercentH = (size.Height / (float)sourceHeight);

        var nPercent = nPercentH < nPercentW ? nPercentH : nPercentW;

        var destWidth = (int)(sourceWidth * nPercent);
        var destHeight = (int)(sourceHeight * nPercent);

        var src = imgToResize;

        using (var dst = new Bitmap(destWidth, destHeight, imgToResize.PixelFormat))
        {

            if (imgToResize.HorizontalResolution != 0 && imgToResize.VerticalResolution != 0)
            {
                dst.SetResolution(imgToResize.HorizontalResolution, imgToResize.VerticalResolution);
            }
            else
            {
                dst.SetResolution(destWidth * 100, destHeight * 100);
            }

            using (var g = Graphics.FromImage(dst))
            {
                g.CompositingQuality = CompositingQuality.HighSpeed;
                g.InterpolationMode = InterpolationMode.Low;
                g.SmoothingMode = SmoothingMode.HighSpeed;
                g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                var format = ImageFormat.Jpeg;
                g.DrawImage(src, 0, 0, dst.Width, dst.Height);

                var m = new MemoryStream();
                dst.Save(m, format);

                var img = (Bitmap)Image.FromStream(m);

                return img;
            }
        }
    }

}
