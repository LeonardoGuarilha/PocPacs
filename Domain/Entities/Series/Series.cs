using Domain.ValueObjects;
using Flunt.Validations;
using Shared.Core.DomainObjects;

namespace Domain.Entities.Series;

public class Series : EntityBase
{
    public string SeriesId { get; private set; }
    public StudyUidVO StudyUidFKey { get; private set; }

    public Series(string seriesId, StudyUidVO studyUidFkey)
    {
        StudyUidFKey = studyUidFkey;
        SeriesId = seriesId;

       AddNotifications(new Contract()
            .Requires()
            .IsNotNullOrEmpty("ImageUid", SeriesId, "Id da serie deve estar preenchido")
        );

    }
}
