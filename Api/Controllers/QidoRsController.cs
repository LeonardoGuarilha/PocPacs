using Application.Handlers.QueryHandlers.Studies.ListStudies;
using Domain.Model;
using Domain.SearchableRepository;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Mediator;

namespace Api.Controllers;

[Route("api/qidors")]
[ApiController]
public class QidoRSController : ControllerBase
{
    private readonly IMediatorHandler _mediator;

    public QidoRSController(IMediatorHandler mediator)
    {
        _mediator = mediator;
    }

    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status200OK)]
    [HttpPost("studies")]
    public async Task<IActionResult> GetStudies(
        CancellationToken cancellationToken,
        [FromBody] GetStudiesModel inputStudies,
        [FromQuery] int? page = null,
        [FromQuery(Name = "perPage")] int? perPage = null,
        [FromQuery] SearchOrder? sortOrder = null,
        [FromQuery] string? sortField = null
    )
    {
        var input = new ListStudiesInput();

        if (!String.IsNullOrEmpty(inputStudies.AcNumber))
            input.AcNumber = inputStudies.AcNumber;

        if (!String.IsNullOrEmpty(inputStudies.InitialDate))
            input.InitialDate = inputStudies.InitialDate;

        if (!String.IsNullOrEmpty(inputStudies.FinalDate))
            input.FinalDate = inputStudies.FinalDate;

        if (!String.IsNullOrEmpty(inputStudies.StudyDescription))
            input.StudyDescription = inputStudies.StudyDescription;

        if (!String.IsNullOrEmpty(inputStudies.PatientId))
            input.PatientId = inputStudies.PatientId;

        if (!String.IsNullOrEmpty(inputStudies.Modality))
            input.Modality = inputStudies.Modality;

        if (!String.IsNullOrEmpty(inputStudies.PatientBirthdate))
            input.PatientBirthdate = inputStudies.PatientBirthdate;

        if (!String.IsNullOrEmpty(inputStudies.PatientName))
            input.PatientName = inputStudies.PatientName;

        if (page is not null)
            input.Page = page.Value;

        if (perPage is not null)
            input.PerPage = perPage.Value;

        if (sortOrder is not null)
            input.Dir = sortOrder.Value;

        if (!String.IsNullOrWhiteSpace(sortField))
            input.Sort = sortField;

        var output = await _mediator.SendCommand(input);

        if (!output.Success)
        {
            return BadRequest(output);
        }

        return Ok(output);
    }
}
