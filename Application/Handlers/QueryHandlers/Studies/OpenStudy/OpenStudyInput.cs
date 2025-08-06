using Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Handlers.QueryHandlers.Studies.OpenStudy;

// Primary Contructor
public class OpenStudyInput(StudyUidVO studyUid, string serieInstanceUid, string instanceUid, bool thumbnail, bool metadata) : IRequest<IActionResult>
{
    public StudyUidVO StudyUid { get; set; } = studyUid;
    public string SerieInstanceUid { get; set; } = serieInstanceUid;
    public string InstanceUid { get; set; } = instanceUid;
    public bool Thumbnail { get; set; } = thumbnail;
    public bool Metadata { get; set; } = metadata;
}
