using Dicom.Imaging;
using Microsoft.AspNetCore.Mvc;

namespace Application.Handlers.QueryHandlers.Studies.OpenStudy;

public class OpenStudy : IOpenStudy
{
    public Task<IActionResult> Handle(OpenStudyInput request, CancellationToken cancellationToken)
    {
        ImageManager.SetImplementation(WinFormsImageManager.Instance);

        throw new NotImplementedException();
    }
}
