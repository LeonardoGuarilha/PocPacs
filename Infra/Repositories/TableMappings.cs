using System;
using Domain.Dicom;

namespace Infra.Repositories;

public static class TableMappings
{
    private static Func<DicomField, string> BuildEqualTextFilterFunction(string fieldName)
    {
        Func<DicomField, string> EqualTextFilter = (value) =>
        {
            string filter = string.IsNullOrWhiteSpace(value?.Value?.ToString()) ?
                            "" : string.Format(" AND {0} = '{1}' ", fieldName, value.Value);
            return filter;
        };

        return EqualTextFilter;
    }
    private static Func<DicomField, string> BuildLikeTextFilterFunction(string fieldName)
    {
        Func<DicomField, string> LikeTextFilter = (value) =>
        {
            string filter = "";

            if (!string.IsNullOrWhiteSpace(value?.Value?.ToString()) && value?.Value?.ToString() != "*")
            {
                filter = string.Format(" AND {0} LIKE '{1}' ", fieldName, value.Value.ToString().Replace("*", "%"));

                if (value.Value.ToString().Replace("*", "%") == "%^%")
                    filter = String.Empty;

            }

            return filter;
        };

        return LikeTextFilter;
    }
    private static Func<DicomField, string> ReturnModalities(string fieldName)
    {
        Func<DicomField, string> ModalitiesFilter = (value) =>
        {
            string filter = "";

            if (!string.IsNullOrWhiteSpace(value?.Value?.ToString()) && value?.Value?.ToString() != "*")
            {
                filter = string.Format(" AND EXISTS (SELECT S1.SERIES_UID FROM DS_SERIESTABLE S1 WHERE S1.STUDYUID_FKEY = DS_STUDYTABLE.STUDY_UID AND S1.ID_UNIDADE = DS_STUDYTABLE.ID_UNIDADE AND S1.MODALITY IN ({0})) ", string.Join(",", value.Value.ToString().Split("\\").Select(item => "'" + item + "'")));
            }

            return filter;
        };

        return ModalitiesFilter;
    }
    private static string FormatDate(string unformattedDate)
    {
        var MIN_DATE = "00010101";

        if (unformattedDate.Length < 8)
        {
            return MIN_DATE;
        }

        var year = unformattedDate.Substring(0, 4);
        var month = unformattedDate.Substring(4, 2);
        var day = unformattedDate.Substring(6, 2);

        if (!int.TryParse(year, out int value))
        {
            return MIN_DATE;
        }

        var isLeapYear = (value % 4 == 0) || (value % 400 == 0);
        var monthLens = new List<int> { 0, 31, isLeapYear ? 29 : 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        if (value < 0)
        {
            return MIN_DATE;
        }

        if (!int.TryParse(month, out value))
        {
            return MIN_DATE;
        }

        var iMonth = value;

        if (!int.TryParse(day, out value))
        {
            return MIN_DATE;
        }

        var iDay = value;

        if (iMonth < 1 || iMonth > 12)
        {
            return MIN_DATE;
        }

        if (iDay < 0 || iDay > monthLens[iMonth])
        {
            return MIN_DATE;
        }

        return string.Format("{0}-{1}-{2}", year, month, day);
    }

    private static Func<DicomField, string> BuildBetweenDateFilterFunction(string fieldName)
    {

        Func<DicomField, string> BetweenDateFilter = (value) =>
        {

            // Incluido o if por causa das consultas da carestream que o formato de buscar é fora do padrão
            if (value.Value.ToString().Count() == 16)
            {
                var FILTER = "AND {0} between TO_DATE('{1}', 'YYYY-MM-DD') and TO_DATE('{2}', 'YYYY-MM-DD')";
                var dates1 = value.Value.ToString().Substring(0, 8);
                var dates2 = value.Value.ToString().Substring(9, 8);
                var initialDate = FormatDate(dates1);
                var finalDate = FormatDate(dates2);

                return string.Format(FILTER, fieldName, initialDate, finalDate);
            }
            if (value.Value.ToString().Count() == 8)
            {
                var FILTER = "AND {0} between TO_DATE('{1} 00:00:00', 'YYYY-MM-DD HH24:MI:SS') and TO_DATE('{2} 23:59:59', 'YYYY-MM-DD HH24:MI:SS')";
                var dates1 = value.Value.ToString();
                DateTime date = DateTime.ParseExact(value.Value.ToString(), "yyyyMMdd", null);
                var dates2 = date;
                var initialDate = FormatDate(dates1);
                var finalDate = FormatDate(dates2.ToString("yyyyMMdd"));

                return string.Format(FILTER, fieldName, initialDate, finalDate);
            }
            else
            {
                var MAX_DATE = "99991231";
                var FILTER = "AND {0} between TO_DATE('{1}', 'YYYY-MM-DD') and TO_DATE('{2}', 'YYYY-MM-DD')";
                var dates = value.Value.ToString().Split('-');
                var initialDate = dates[0];
                var finalDate = dates.Length > 1 ? dates[1] : MAX_DATE;

                initialDate = FormatDate(dates[0]);
                finalDate = FormatDate(dates[1]);

                return string.Format(FILTER, fieldName, initialDate, finalDate);
            }
        };

        return BetweenDateFilter;
    }

    public static Dictionary<string, Func<DicomField, string>> StudyTable = new Dictionary<string, Func<DicomField, string>>() {
        { "StudyID", BuildEqualTextFilterFunction("STUDYID")},
        { "AccessionNumber", BuildLikeTextFilterFunction("ACCESSIONNUMBER")},
        { "PatientID", BuildLikeTextFilterFunction("PATIENTID_FKEY")},
        { "StudyDate", BuildBetweenDateFilterFunction("STUDYDATE")},
        { "PatientName", BuildLikeTextFilterFunction("PATIENTNAME_DICOM")},
        { "ModalitiesInStudy", ReturnModalities("PATIENTNAME_DICOM")},
        { "ReferringPhysicianName", BuildLikeTextFilterFunction("REFERRINGPHYSICIAN_DICOM")},
        { "StudyDescription", BuildLikeTextFilterFunction("STUDYDESCRIPTION")},
        { "PatientBirthDate", BuildBetweenDateFilterFunction("DATEOFBIRTH")},
        { "StudyInstanceUID", BuildEqualTextFilterFunction("STUDY_UID")},
        //TAG DICOM

        { "00200010", BuildEqualTextFilterFunction("STUDYID")},
        { "00080050", BuildLikeTextFilterFunction("ACCESSIONNUMBER")},
        { "00100020", BuildLikeTextFilterFunction("PATIENTID_FKEY")},
        { "00080020", BuildBetweenDateFilterFunction("STUDYDATE")},
        { "00100010", BuildLikeTextFilterFunction("PATIENTNAME_DICOM")},
        { "00080062", ReturnModalities("PATIENTNAME_DICOM")},
        { "00080090", BuildLikeTextFilterFunction("REFERRINGPHYSICIAN_DICOM")},
        { "00081030", BuildLikeTextFilterFunction("STUDYDESCRIPTION")},
        { "00100030", BuildBetweenDateFilterFunction("DATEOFBIRTH")},
        { "0020000d", BuildEqualTextFilterFunction("STUDY_UID")},
        { "0020000D", BuildEqualTextFilterFunction("STUDY_UID")},

    };

    public static Dictionary<string, Func<DicomField, string>> SeriesTable = new Dictionary<string, Func<DicomField, string>>() {
        //{ "SeriesInstanceUID", BuildEqualTextFilterFunction("SERIES_UID")},
        //{ "StudyInstanceUID", BuildEqualTextFilterFunction("STUDYUID_FKEY")},

        //{ "0020000e", BuildEqualTextFilterFunction("SERIES_UID")},
        //{ "0020000d", BuildEqualTextFilterFunction("STUDYUID_FKEY")},
        //{ "0020000E", BuildEqualTextFilterFunction("SERIES_UID")},
        //{ "0020000D", BuildEqualTextFilterFunction("STUDYUID_FKEY")},
        { "SeriesInstanceUID", BuildEqualTextFilterFunction("SERIES_UID")},
        { "StudyInstanceUID", BuildEqualTextFilterFunction("STUDY_UID")},

        { "0020000e", BuildEqualTextFilterFunction("SERIES_UID")},
        { "0020000d", BuildEqualTextFilterFunction("STUDY_UID")},
        { "0020000E", BuildEqualTextFilterFunction("SERIES_UID")},
        { "0020000D", BuildEqualTextFilterFunction("STUDY_UID")},


    };

    public static Dictionary<string, Func<DicomField, string>> InstanceTable = new Dictionary<string, Func<DicomField, string>>() {
        //{ "SOPInstanceUID", BuildEqualTextFilterFunction("INSTANCEUID")},
        //{ "SeriesInstanceUID", BuildEqualTextFilterFunction("SERIESUID_FKEY")},
        //{ "StudyInstanceUID", BuildEqualTextFilterFunction("STUDYUID_FKEY")},

        //{ "00080018", BuildEqualTextFilterFunction("INSTANCEUID")},
        //{ "0020000e", BuildEqualTextFilterFunction("SERIESUID_FKEY")},
        //{ "0020000d", BuildEqualTextFilterFunction("STUDYUID_FKEY")},
        //{ "0020000E", BuildEqualTextFilterFunction("SERIESUID_FKEY")},
        //{ "0020000D", BuildEqualTextFilterFunction("STUDYUID_FKEY")},
        { "SOPInstanceUID", BuildEqualTextFilterFunction("INSTANCEUID")},
        { "SeriesInstanceUID", BuildEqualTextFilterFunction("SERIES_UID")},
        { "StudyInstanceUID", BuildEqualTextFilterFunction("STUDY_UID")},

        { "00080018", BuildEqualTextFilterFunction("INSTANCEUID")},
        { "0020000e", BuildEqualTextFilterFunction("SERIES_UID")},
        { "0020000d", BuildEqualTextFilterFunction("STUDY_UID")},
        { "0020000E", BuildEqualTextFilterFunction("SERIES_UID")},
        { "0020000D", BuildEqualTextFilterFunction("STUDY_UID")},

    };

    public static Dictionary<string, Func<DicomField, string>> PatientTable = new Dictionary<string, Func<DicomField, string>>() {
        { "PatientName", BuildLikeTextFilterFunction("PATIENTNAME_DICOM")},
        { "PatientID", BuildLikeTextFilterFunction("PATIENTID_FKEY")},
        { "PatientsBirthDate", BuildEqualTextFilterFunction("DATEOFBIRTH")},
        { "AccessionNumber", BuildLikeTextFilterFunction("ACCESSIONNUMBER")},
        { "00100010", BuildLikeTextFilterFunction("PATIENTNAME_DICOM")},
        { "00100020", BuildLikeTextFilterFunction("PATIENTID_FKEY")},
        { "00100030", BuildEqualTextFilterFunction("DATEOFBIRTH")},
        { "00080050", BuildLikeTextFilterFunction("ACCESSIONNUMBER")},
    };

    public static Dictionary<string, Func<DicomField, string>> WorkListTable = new Dictionary<string, Func<DicomField, string>>() {
        { "AccessionNumber", BuildLikeTextFilterFunction("accessionnumber")},
        { "RequestingPhysician", BuildLikeTextFilterFunction("requestingphysician")},
        { "PatientID", BuildLikeTextFilterFunction("schedpatientid")},
        { "PatientName", BuildLikeTextFilterFunction("schedpatientname")},
        { "Modality", BuildLikeTextFilterFunction("modality")},
        { "ScheduledStationAETitle", BuildLikeTextFilterFunction("schedstationaetitle")},
        { "ReferringPhysicianName", BuildLikeTextFilterFunction("refferingphysician")},
        { "ScheduledPerformingPhysicianName", BuildLikeTextFilterFunction("schedprocperformingphy")},
        { "ScheduledProcedureStepStartDate", BuildBetweenDateFilterFunction("schedprocstartdate")},

        { "00080050", BuildLikeTextFilterFunction("accessionnumber")},
        { "00321031", BuildLikeTextFilterFunction("requestingphysician")},
        { "00100020", BuildLikeTextFilterFunction("schedpatientid")},
        { "00100010", BuildLikeTextFilterFunction("schedpatientname")},
        { "00080060", BuildLikeTextFilterFunction("modality")},
        { "00400001", BuildLikeTextFilterFunction("schedstationaetitle")},
        { "00080090", BuildLikeTextFilterFunction("refferingphysician")},
        { "00400006", BuildLikeTextFilterFunction("schedprocperformingphy")},
        { "00400009", BuildBetweenDateFilterFunction("schedprocstartdate")},

    };
}
