using System;
using Dicom;
using Domain.Entities.Image;

namespace Application.Converters;

    public static class InstanceConverter
    {
        public static DicomDataset ToDataset(this Image instance)
        {
            var mapping = new Dictionary<DicomTag, object>
            {
                { DicomTag.AcquisitionNumber, instance.AcquisitionNumber },
                { DicomTag.EchoNumbers, instance.Echonumbers },
                { DicomTag.ImageOrientationPatient, instance.ImageOrientationPatient },
                { DicomTag.ImagePositionPatient, instance.ImagePositionPatient },
                { DicomTag.ImageType, instance.ImageType },
                { DicomTag.InstanceNumber, instance.InstanceNumber },
                { DicomTag.NumberOfFrames, instance.NumberOfFrames },
                { DicomTag.SeriesInstanceUID, instance.SeriesInstanceUid },
                { DicomTag.SliceLocation, instance.SliceLocation },
                { DicomTag.SliceThickness, instance.SliceThickness },
                { DicomTag.SOPClassUID, instance.SopClassUid },
                { DicomTag.SOPInstanceUID, instance.SopInstanceUid },
                { DicomTag.StudyInstanceUID, instance.StudyInstanceUid },
                { DicomTag.Rows,  Convert.ToUInt16(instance.ImageRows) },
                { DicomTag.Columns, Convert.ToUInt16(instance.ImageColumns) }
            };

            return Formatter.MappingToDataset(mapping);
        }

        public static List<DicomDataset> ToDataset(this List<Image> instances)
        {
            return instances.Select(instance => instance.ToDataset()).ToList();
        }

    }
