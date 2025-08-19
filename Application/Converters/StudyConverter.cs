using System;
using Dicom;
using Domain.Entities.Study;

namespace Application.Converters;

    public static class StudyConverter
    {
        public static DicomDataset ToDataset(this Study study)
        {
            var mapping = new Dictionary<DicomTag, object>
            {
                { DicomTag.AccessionNumber, study.AccessionNumber },
                { DicomTag.RetrieveAETitle, study.RemoteAet},
                { DicomTag.PatientID, study.PatientId},
                { DicomTag.PatientBirthDate, study.PatientBirthDate},
                { DicomTag.PatientName, study.PatientName },
                { DicomTag.PatientSex, study.PatientSex },
                { DicomTag.ReferringPhysicianName, study.ReferringPhysiciansName },
                { DicomTag.SpecificCharacterSet, study.SpecificCharacterSet },
                { DicomTag.StudyDate, study.StudyDate.ToString("yyyyMMdd") },
                { DicomTag.StudyDescription, study.StudyDescription },
                { DicomTag.StudyID, study.StudyId },
                { DicomTag.StudyInstanceUID, study.StudyUid.StudyUid },
                { DicomTag.StudyTime, study.StudyTime.ToString("HHmmss") },
                { DicomTag.NumberOfStudyRelatedSeries, study.NumberOfStudyRelatedSeries },
                { DicomTag.NumberOfStudyRelatedInstances, study.NumberOfStudyRelatedInstances },
                { DicomTag.ModalitiesInStudy, study.ModalitiesInStudy },
            };

            return Formatter.MappingToDataset(mapping);
        }

        public static List<DicomDataset> ToDataset(this List<Study> studies)
        {
            return studies.Select(study => study.ToDataset()).ToList();
        }

    }
