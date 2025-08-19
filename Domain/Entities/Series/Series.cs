using Domain.ValueObjects;
using Flunt.Validations;
using Shared.Core.DomainObjects;

namespace Domain.Entities.Series;

public class Series : EntityBase
{
    public string SeriesId { get; private set; }
    public StudyUidVO StudyUidFKey { get; private set; }
    public string Modality { get; private set; }
    public DateTime SeriesDate { get; private set; }
    public DateTime SeriesTime { get; private set; }
    public string SeriesDescription { get; private set; }
    public string SeriesInstanceUid { get; private set; }
    public int SeriesNumber { get; private set; }
    public string NumberOfSeriesRelatedInstances { get; private set; }

    public Series(string seriesId, StudyUidVO studyUidFKey, string modality, DateTime seriesDate, DateTime seriesTime, string seriesDescription, string seriesInstanceUid, int seriesNumber, string numberOfSeriesRelatedInstances)
    {
        SeriesId = seriesId;
        StudyUidFKey = studyUidFKey;
        Modality = modality;
        SeriesDate = seriesDate;
        SeriesTime = seriesTime;
        SeriesDescription = seriesDescription;
        SeriesInstanceUid = seriesInstanceUid;
        SeriesNumber = seriesNumber;
        NumberOfSeriesRelatedInstances = numberOfSeriesRelatedInstances;
    }

}
