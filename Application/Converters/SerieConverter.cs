using Dicom;
using Domain.Entities.Series;

namespace Application.Converters;

    public static class SerieConverter
    {
        public static DicomDataset ToDataset(this Series series)
        {
            var mapping = new Dictionary<DicomTag, object>
            {
                { DicomTag.Modality, series.Modality},
                { DicomTag.SeriesDate, series.SeriesDate != DateTime.MinValue ? series.SeriesDate.ToString("yyyyMMdd"): null},
                { DicomTag.SeriesTime, series.SeriesTime != DateTime.MinValue ? series.SeriesTime.ToString("HHmmss") : null},
                { DicomTag.SeriesDescription, series.SeriesDescription},
                { DicomTag.SeriesInstanceUID, series.SeriesInstanceUid},
                { DicomTag.SeriesNumber, series.SeriesNumber},
                { DicomTag.NumberOfSeriesRelatedInstances, series.NumberOfSeriesRelatedInstances},
                { DicomTag.StudyInstanceUID, series.StudyUidFKey.StudyUid}
            };
            return Formatter.MappingToDataset(mapping);
        }
        public static List<DicomDataset> ToDataset(this List<Series> series)
        {
            return series.Select(serie => serie.ToDataset()).ToList();
        }
    }
