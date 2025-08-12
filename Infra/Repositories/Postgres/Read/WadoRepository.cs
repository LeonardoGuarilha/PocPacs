using System.Text;
using Dapper;
using Domain.Queries;
using Domain.Repositories.Read;
using Infra.ConnectionContext;
using Shared.Core.Result;

namespace Infra.Repositories.Postgres.Read;

public class WadoRepository : IWadoRepository
{
    private readonly IConnectionProvider _connectionProvider;

    public WadoRepository(IConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }


    public async Task<Result<GetMetadata>> RetrieveMetadata(string? studyUid, string? seriesUid, string? instanceUid, int idUnidade)
    {
        StringBuilder stringBuilder = new StringBuilder();
        DynamicParameters dynamicParameters = new DynamicParameters();

        stringBuilder.AppendLine("SELECT /*+ INDEX (DS_ImageTable imagetable_pk) */");
        stringBuilder.AppendLine("(SELECT CONCAT(p.ALIAS_PATH_NAME, I.FILENAME) FROM DS_PATHNAMES P WHERE P.ID_PATH_NAME = I.ID_PATH_NAME) AS PathName,");
        stringBuilder.AppendLine("(SELECT PATH_TYPE FROM DS_PATHNAMES P WHERE P.ID_PATH_NAME = I.ID_PATH_NAME) AS PathType, ");
        stringBuilder.AppendLine("(SELECT DISK_CLIENTID FROM DS_PATHNAMES P WHERE P.ID_PATH_NAME = I.ID_PATH_NAME) AS DiskClientId, ");
        stringBuilder.AppendLine("(SELECT DISK_SECRET FROM DS_PATHNAMES P WHERE P.ID_PATH_NAME = I.ID_PATH_NAME) AS DiskSecret, ");
        stringBuilder.AppendLine("(SELECT DISK_BUCKET FROM DS_PATHNAMES P WHERE P.ID_PATH_NAME = I.ID_PATH_NAME) AS DiskBucket, ");
        stringBuilder.AppendLine("(SELECT DISK_REGION FROM DS_PATHNAMES P WHERE P.ID_PATH_NAME = I.ID_PATH_NAME) AS DiskRegion, ");
        stringBuilder.AppendLine("S.SERIESDESCRIPTION AS SerieDescription,");
        stringBuilder.AppendLine("S.STUDYUID_FKEY AS StudyUid,");
        stringBuilder.AppendLine("S.SERIES_UID AS SerieUid,");
        stringBuilder.AppendLine("I.INSTANCEUID AS InstanceUid,");
        stringBuilder.AppendLine("T.PATIENTNAME_DICOM AS PatientName,");
        stringBuilder.AppendLine("T.PATIENTID_FKEY AS PatientId,");
        stringBuilder.AppendLine("TO_CHAR(T.STUDYTIME, 'DD/MM/YYYY') AS StudyDate,"); // Formatted StudyDate
        stringBuilder.AppendLine("T.ACCESSIONNUMBER AS AccessionNumber,");
        stringBuilder.AppendLine("T.DATEOFBIRTH AS PatientBirth,");
        stringBuilder.AppendLine("S.MODALITY AS Modality,");
        stringBuilder.AppendLine("TO_CHAR(S.SERIESTIME, 'DD/MM/YYYY HH24:MI:SS') AS SeriesTime "); // Formatted SeriesTime
        stringBuilder.AppendLine("FROM DS_STUDYTABLE T ");
        stringBuilder.AppendLine("INNER JOIN DS_SERIESTABLE S ON T.STUDY_UID = S.STUDYUID_FKEY AND T.ID_UNIDADE = S.ID_UNIDADE ");
        stringBuilder.AppendLine("INNER JOIN DS_IMAGETABLE I ON T.ID_UNIDADE = I.ID_UNIDADE AND T.STUDY_UID = I.STUDYUID_FKEY AND S.SERIES_UID = I.SERIESUID_FKEY ");

        if (!string.IsNullOrEmpty(instanceUid))
        {
            stringBuilder.AppendLine("WHERE I.STUDYUID_FKEY = :StudyUid AND I.SERIESUID_FKEY = :SeriesUid AND I.INSTANCEUID = :InstanceUid AND I.ID_UNIDADE = :IdUnidade");
            dynamicParameters.Add(":StudyUid", studyUid);
            dynamicParameters.Add(":SeriesUid", seriesUid);
            dynamicParameters.Add(":InstanceUid", instanceUid);
            dynamicParameters.Add(":IdUnidade", idUnidade);
        }
        else if (!string.IsNullOrEmpty(seriesUid))
        {
            stringBuilder.AppendLine("WHERE I.STUDYUID_FKEY = :StudyUid AND I.SERIESUID_FKEY = :SeriesUid AND I.ID_UNIDADE = :IdUnidade");
            dynamicParameters.Add(":StudyUid", studyUid);
            dynamicParameters.Add(":SeriesUid", seriesUid);
            dynamicParameters.Add(":IdUnidade", idUnidade);
        }
        else
        {
            stringBuilder.AppendLine("WHERE I.STUDYUID_FKEY = :StudyUid AND I.ID_UNIDADE = :IdUnidade");
            dynamicParameters.Add(":StudyUid", studyUid);
            dynamicParameters.Add(":IdUnidade", idUnidade);
        }

        stringBuilder.AppendLine("ORDER BY I.INSTANCENUMBER");

        using (var connection = _connectionProvider.CreateConnection())
        {
            try
            {
                var result = await connection.QueryFirstOrDefaultAsync<GetMetadata>(stringBuilder.ToString(), dynamicParameters);

                return Result.Success<GetMetadata>(result);
            }
            catch (Exception ex)
            {
                return Result.Failure<GetMetadata>(Error.NullValue);
            }
        }
    }

    public async Task<Result<GetImage>> RetrieveSopInstance(string? studyUid, string? seriesUid, string? instanceUid, int idUnidade)
    {
        StringBuilder stringBuilder = new StringBuilder();
        DynamicParameters dynamicParameters = new DynamicParameters();

        stringBuilder.AppendLine("SELECT /*+ INDEX (DS_ImageTable imagetable_pk) */");
        stringBuilder.AppendLine("(SELECT CONCAT(p.ALIAS_PATH_NAME, I.FILENAME) FROM DS_PATHNAMES P WHERE P.ID_PATH_NAME = I.ID_PATH_NAME) AS PathName,");
        stringBuilder.AppendLine("(SELECT PATH_TYPE FROM DS_PATHNAMES P WHERE P.ID_PATH_NAME = I.ID_PATH_NAME) AS PathType, ");
        stringBuilder.AppendLine("(SELECT DISK_CLIENTID FROM DS_PATHNAMES P WHERE P.ID_PATH_NAME = I.ID_PATH_NAME) AS DiskClientId, ");
        stringBuilder.AppendLine("(SELECT DISK_SECRET FROM DS_PATHNAMES P WHERE P.ID_PATH_NAME = I.ID_PATH_NAME) AS DiskSecret, ");
        stringBuilder.AppendLine("(SELECT DISK_BUCKET FROM DS_PATHNAMES P WHERE P.ID_PATH_NAME = I.ID_PATH_NAME) AS DiskBucket, ");
        stringBuilder.AppendLine("(SELECT DISK_REGION FROM DS_PATHNAMES P WHERE P.ID_PATH_NAME = I.ID_PATH_NAME) AS DiskRegion, ");
        stringBuilder.AppendLine("S.SERIESDESCRIPTION AS SerieDescription,");
        stringBuilder.AppendLine("S.STUDYUID_FKEY AS StudyUid,");
        stringBuilder.AppendLine("S.SERIES_UID AS SerieUid,");
        stringBuilder.AppendLine("I.INSTANCEUID AS InstanceUid,");
        stringBuilder.AppendLine("T.PATIENTNAME_DICOM AS PatientName,");
        stringBuilder.AppendLine("T.PATIENTID_FKEY AS PatientId,");
        stringBuilder.AppendLine("TO_CHAR(T.STUDYTIME, 'DD/MM/YYYY') AS StudyDate,"); // Formatted StudyDate
        stringBuilder.AppendLine("T.ACCESSIONNUMBER AS AccessionNumber,");
        stringBuilder.AppendLine("T.DATEOFBIRTH AS PatientBirth,");
        stringBuilder.AppendLine("S.MODALITY AS Modality,");
        stringBuilder.AppendLine("TO_CHAR(S.SERIESTIME, 'DD/MM/YYYY HH24:MI:SS') AS SeriesTime "); // Formatted SeriesTime
        stringBuilder.AppendLine("FROM DS_STUDYTABLE T ");
        stringBuilder.AppendLine("INNER JOIN DS_SERIESTABLE S ON T.STUDY_UID = S.STUDYUID_FKEY AND T.ID_UNIDADE = S.ID_UNIDADE ");
        stringBuilder.AppendLine("INNER JOIN DS_IMAGETABLE I ON T.ID_UNIDADE = I.ID_UNIDADE AND T.STUDY_UID = I.STUDYUID_FKEY AND S.SERIES_UID = I.SERIESUID_FKEY ");

        if (!string.IsNullOrEmpty(instanceUid))
        {
            stringBuilder.AppendLine("WHERE I.STUDYUID_FKEY = :StudyUid AND I.SERIESUID_FKEY = :SeriesUid AND I.INSTANCEUID = :InstanceUid AND I.ID_UNIDADE = :IdUnidade");
            dynamicParameters.Add(":StudyUid", studyUid);
            dynamicParameters.Add(":SeriesUid", seriesUid);
            dynamicParameters.Add(":InstanceUid", instanceUid);
            dynamicParameters.Add(":IdUnidade", idUnidade);
        }
        else if (!string.IsNullOrEmpty(seriesUid))
        {
            stringBuilder.AppendLine("WHERE I.STUDYUID_FKEY = :StudyUid AND I.SERIESUID_FKEY = :SeriesUid AND I.ID_UNIDADE = :IdUnidade");
            dynamicParameters.Add(":StudyUid", studyUid);
            dynamicParameters.Add(":SeriesUid", seriesUid);
            dynamicParameters.Add(":IdUnidade", idUnidade);
        }
        else
        {
            stringBuilder.AppendLine("WHERE I.STUDYUID_FKEY = :StudyUid AND I.ID_UNIDADE = :IdUnidade");
            dynamicParameters.Add(":StudyUid", studyUid);
            dynamicParameters.Add(":IdUnidade", idUnidade);
        }

        stringBuilder.AppendLine("ORDER BY I.INSTANCENUMBER");

        using (var connection = _connectionProvider.CreateConnection())
        {
            try
            {
                var result = await connection.QueryFirstOrDefaultAsync<GetImage>(stringBuilder.ToString(), dynamicParameters);

                return Result.Success<GetImage>(result);
            }
            catch (Exception)
            {
                return Result.Failure<GetImage>(Error.NullValue);
            }
        }
    }
}
