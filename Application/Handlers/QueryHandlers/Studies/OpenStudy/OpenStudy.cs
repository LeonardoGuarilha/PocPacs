using Dicom.Imaging;
using Domain.Repositories.Read;
using Microsoft.AspNetCore.Mvc;

namespace Application.Handlers.QueryHandlers.Studies.OpenStudy;

public class OpenStudy : IOpenStudy
{
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
}
