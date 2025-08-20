using System.Text;
using Dapper;
using Domain.Dicom;
using Domain.Entities.Image;
using Domain.Entities.Series;
using Domain.Entities.Study;
using Domain.Repositories.Read;
using Infra.ConnectionContext;
using Infra.Enum;
using Infra.Extensions;
using Shared.Core.Result;

namespace Infra.Repositories.Postgres.Read;

public class DicomWebRepository : IDicomWebRepostiroy
{
    private readonly IConnectionProvider _connectionProvider;

    public DicomWebRepository(IConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    // TODO: Trocar os retornos para Result<>
    public Result<List<Study>> FindStudy(List<DicomField> dataset)
    {
        var query = @"SELECT DISTINCT Study_Uid StudyInstanceUid,
					  SpecificCharacterSet,
					  PatientsName,
					  PatientId,
					  PatientsBirthDate,
					  PatientsSex,
					  StudyDate,
					  StudyTime,
					  AccessionNumber,
					  StudyId,
					  StudyDescription,
					  ReferringPhysiciansName,
					  ID_UNIDADE Unidade,
					  RemoteAet,
					  NumberOfStudyRelatedSeries,
					  NumberOfStudyRelatedInstances,
					  ModalitiesInStudy
			  FROM DS_FULLVIEW WHERE ID_UNIDADE IS NOT NULL ";

        using (var connection = _connectionProvider.CreateConnection())
        {
            try
            {
                var result = connection.Query<Study>(query + dataset.GetConditions(QueryLevel.Study) + "").AsList(); // o "" seria o orderBy
                return Result.Success<List<Study>>(result.ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Result.Failure<List<Study>>(Error.NoData);
            }
        }
    }

    public Result<List<Series>> FindSeries(List<DicomField> dataset)
    {
        var query = @"SELECT DISTINCT
					Study_Uid StudyInstanceUid,
					Series_Uid SeriesInstanceUid,
					Modality,
					SeriesDate,
					SeriesTime,
					SeriesNumber,
					SeriesDescription,
                    NumberOfSeriesRelatedInstances
			  FROM DS_FULLVIEW
			  WHERE ID_UNIDADE IS NOT NULL ";

        using (var connection = _connectionProvider.CreateConnection())
        {
            try
            {
                var result = connection.Query<Series>(query + dataset.GetConditions(QueryLevel.Series) + "").AsList(); // o "" seria o orderBy
                return Result.Success<List<Series>>(result.ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Result.Failure<List<Series>>(Error.NoData);
            }
        }
    }

    public Result<Image> FindInstance(List<DicomField> dataset)
    {
        var query = @"SELECT DISTINCT Study_Uid StudyInstanceUid,
                    Series_Uid SeriesInstanceUid,
                    InstanceUid SopInstanceUid,
                    InstanceNumber,
                    SopClassUid,
                    ImageType,
                    Echonumbers,
                    SliceThickness,
                    AcquisitionNumber,
                    SliceLocation,
                    ImageOrientationPatient,
                    ImagePositionPatient,
                    ImageRows,
                    ImageKey,
                    ImageColumns,
                    NumberOfFrames,
                    FullFilename,
                    Filename,
                    Sizeondisk,
                    alias_path_name AliasPathName,
                    alias_path_center AliasPathCenter
            FROM DS_FULLVIEW
            WHERE ID_UNIDADE IS NOT NULL ";

            using (var connection = _connectionProvider.CreateConnection())
            {
                try
                {
                    var result = connection.QueryFirstOrDefault<Image>(query + dataset.GetConditions(QueryLevel.Instance) + " ORDER BY ACQUISITIONNUMBER");
                    return Result.Success<Image>(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return Result.Failure<Image>(Error.NoData);
                }
            }
    }

    public Result<List<Image>> FindInstancesThumb(List<DicomField> dataset)
    {
        var query = @"SELECT
             DISTINCT FullFilename,
             Filename,
             Sizeondisk
        FROM DS_FULLVIEW
        WHERE INSTANCENUMBER <= 1 AND ID_UNIDADE IS NOT NULL ";

        using (var connection = _connectionProvider.CreateConnection())
        {
            try
            {
                var result = connection.Query<Image>(query + dataset.GetConditions(QueryLevel.Series) + "").AsList();
                return Result.Success<List<Image>>(result.ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Result.Failure<List<Image>>(Error.NoData);
            }
        }
    }

    public Result<List<Image>> FindInstances(List<DicomField> dataset)
    {
        var query = @"SELECT DISTINCT Study_Uid StudyInstanceUid,
                    Series_Uid SeriesInstanceUid,
                    InstanceUid SopInstanceUid,
                    InstanceNumber,
                    SopClassUid,
                    ImageType,
                    Echonumbers,
                    SliceThickness,
                    AcquisitionNumber,
                    SliceLocation,
                    ImageOrientationPatient,
                    ImagePositionPatient,
                    ImageRows,
                    ImageKey,
                    ImageColumns,
                    NumberOfFrames,
                    FullFilename,
                    Filename,
                    Sizeondisk,
                    alias_path_name AliasPathName,
                    alias_path_center AliasPathCenter
            FROM DS_FULLVIEW
            WHERE ID_UNIDADE IS NOT NULL ";

            using (var connection = _connectionProvider.CreateConnection())
            {
                try
                {
                    var result = connection.Query<Image>(query + dataset.GetConditions(QueryLevel.Instance) + " ORDER BY ACQUISITIONNUMBER").AsList();
                    return Result.Success<List<Image>>(result.ToList());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return Result.Failure<List<Image>>(Error.NoData);
                }
            }
    }
}
