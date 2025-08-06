using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Handlers.QueryHandlers.Studies.OpenStudy;

public interface IOpenStudy : IRequestHandler<OpenStudyInput, IActionResult>
{

}
