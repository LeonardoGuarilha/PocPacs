using Dicom;

namespace Application.Converters;

public static class Formatter
{
    public static DicomDataset MappingToDataset(Dictionary<DicomTag, object> mapping)
    {
        var dataset = new DicomDataset();

        foreach (var entry in mapping)
        {
            try
            {
                if (entry.Value == null)
                {
                    dataset.Add(entry.Key, "");
                }
                else if (entry.Value.GetType() == typeof(String))
                {
                    dataset.Add(entry.Key, entry.Value.ToString());
                }
                else if (entry.Value.GetType() == typeof(Int16))
                {
                    dataset.Add(entry.Key, entry.Value.ToString());
                }
                else if (entry.Value.GetType() == typeof(Int32))
                {
                    dataset.Add(entry.Key, entry.Value.ToString());
                }
                else if (entry.Value.GetType() == typeof(DateTime))
                {
                    dataset.Add(DicomVR.DA, entry.Key, ((DateTime)entry.Value).ToString("yyyyMMdd"));
                }
                else if (entry.Value.GetType() == typeof(TimeSpan))
                {
                    dataset.Add(DicomVR.TM, entry.Key, ((TimeSpan)entry.Value).ToString("hhmmss"));
                }
                else if (entry.Value.GetType() == typeof(Dictionary<DicomTag, object>))
                {
                    dataset.Add(entry.Key, MappingToDataset((Dictionary<DicomTag, object>)entry.Value));
                }
                else
                {
                    dataset.Add(entry.Key, entry.Value.ToString());
                }
            }
            catch
            {
                continue;
            }
        }

        return dataset;
    }
}
