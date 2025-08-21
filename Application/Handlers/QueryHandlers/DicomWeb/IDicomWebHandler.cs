using System;
using Domain.Dicom;
using Domain.Entities.Image;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Result;

namespace Application.Handlers.QueryHandlers.DicomWeb;

public interface IDicomWebHandler
{
    IActionResult RetrieveStudies(List<DicomField> request);
    IActionResult RetrieveSeries(List<DicomField> request, string studyInstanceUID);
    IActionResult GetSeriesThumbnail(string studyInstanceUID, string seriesInstanceUID);
    IActionResult RetrieveInstances(List<DicomField> request, string studyInstanceUID, string seriesInstanceUID);
    MemoryStream GetInstanceStream(Image instance, string studyInstanceUID, string seriesInstanceUID);
    Result<Image> FindInstance(List<DicomField> dataset);
}
