using System.Text;
using Dapper;
using Domain.Entities.Study;
using Domain.Model;
using Domain.Queries;
using Domain.Repositories.Read;
using Domain.SearchableRepository;
using Infra.ConnectionContext;
using Shared.Core.Result;

namespace Infra.Repositories.Postgres.Read;

public class StudiesReadRepository : IStudyRepository
{
    private readonly IConnectionProvider _connectionProvider;

    public StudiesReadRepository(IConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async Task<Result<List<GetAllStudies>>> GetAllStudies(GetStudiesModel input, int idUnidade)
    {
        StringBuilder stringBuilder = new StringBuilder();
        DynamicParameters dynamicParameters = new DynamicParameters();

        stringBuilder.Append("SELECT DISTINCT ds.STUDY_UID AS StudyUid, ds.ACCESSIONNUMBER AS AcNumber, ds.PATIENTID_FKEY AS PatientId, ds.PATIENTNAME_DICOM AS PatientName, ");
        stringBuilder.Append("TO_CHAR(ds.STUDYDATE, 'DD/MM/YYYY') AS StudyDate, ds.STUDYDESCRIPTION AS StudyDescription, ");
        stringBuilder.Append("FUNCTIONMODALITIESINSTUDY(ds.STUDY_UID, ds.ID_UNIDADE) AS Modality, (SELECT count(*) FROM ");
        stringBuilder.Append("DS_SERIESTABLE a WHERE a.STUDYUID_FKEY  = ds.STUDY_UID) Series, ");
        stringBuilder.Append("COALESCE(ds.SEX, 'O') AS PatientSex, ds.DATEOFBIRTH AS PatientBirth, di.DS_JOBERRORDESCRIPTION AS Message, di.TP_JOBSTATUS AS Status ");
        stringBuilder.Append("FROM DS_STUDYTABLE ds ");
        stringBuilder.Append("INNER JOIN DS_SERIESTABLE ds2 ON ds.STUDY_UID = ds2.STUDYUID_FKEY ");
        stringBuilder.Append("LEFT JOIN (SELECT ID_IMAGEJOBS, DS_STUDYUID, TP_JOBSTATUS, DS_JOBERRORDESCRIPTION ");
        stringBuilder.Append("FROM DS_IMAGEJOBS ");
        stringBuilder.Append("WHERE ID_IMAGEJOBS IN (SELECT MAX(ID_IMAGEJOBS) FROM DS_IMAGEJOBS)) di ");
        stringBuilder.Append("ON ds.STUDY_UID = di.DS_STUDYUID ");
        stringBuilder.Append("WHERE ds.ID_UNIDADE = @id_unidade ");

        dynamicParameters.Add("id_unidade", idUnidade);

        if (!String.IsNullOrEmpty(input.AcNumber))
        {
            stringBuilder.Append("AND ds.ACCESSIONNUMBER = @ac_number ");
            dynamicParameters.Add("ac_number", input.AcNumber);
        }

        if (!String.IsNullOrEmpty(input.InitialDate) && !String.IsNullOrEmpty(input.FinalDate))
        {
            stringBuilder.Append("AND ds.STUDYDATE BETWEEN TO_DATE(@initial_date, 'YYYY-MM-DD') AND TO_DATE(@final_date, 'YYYY-MM-DD') ");
            dynamicParameters.Add("initial_date", input.InitialDate);
            dynamicParameters.Add("final_date", input.FinalDate);
        }

        if (!String.IsNullOrEmpty(input.StudyDescription))
        {
            stringBuilder.Append("AND UPPER(ds.STUDYDESCRIPTION) LIKE '%' || @study_description || '%' ");
            dynamicParameters.Add("study_description", input.StudyDescription.ToUpper());
        }

        if (!String.IsNullOrEmpty(input.PatientId))
        {
            stringBuilder.Append("AND ds.PATIENTID_FKEY LIKE '%' || @patient_id || '%' ");
            dynamicParameters.Add("patient_id", input.PatientId);
        }

        if (!String.IsNullOrEmpty(input.Modality))
        {
            stringBuilder.Append("AND ds2.MODALITY = @modality ");
            dynamicParameters.Add("modality", input.Modality);
        }

        if (!String.IsNullOrEmpty(input.PatientBirthdate))
        {
            stringBuilder.Append("AND ds.DATEOFBIRTH = @patient_birthdate::DATE ");
            dynamicParameters.Add("patient_birthdate", input.PatientBirthdate);
        }

        if (!string.IsNullOrEmpty(input.PatientName))
        {
            stringBuilder.Append("AND LOWER(ds.PATIENTNAME_DICOM) LIKE LOWER('%' || @patient_name || '%') ");
            dynamicParameters.Add("patient_name", input.PatientName);
        }

        stringBuilder.Append(" GROUP BY ds.ACCESSIONNUMBER, ds.PATIENTID_FKEY, ds.PATIENTNAME_DICOM, ds.STUDYDATE, ds.STUDYDESCRIPTION, ");
        stringBuilder.Append("ds.STUDY_UID, ds.SEX, ds.DATEOFBIRTH, di.DS_JOBERRORDESCRIPTION, di.TP_JOBSTATUS, ds.ID_UNIDADE");

        using (var connection = _connectionProvider.CreateConnection())
        {
            try
            {
                var result = await connection.QueryAsync<GetAllStudies>(stringBuilder.ToString(), dynamicParameters);
                return Result.Success<List<GetAllStudies>>(result.ToList());
            }
            catch (Exception)
            {
                return Result.Failure<List<GetAllStudies>>(Error.NoData);
            }
        }
    }

    public Task<SearchOutput<Study>> Search(SearchInput input, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
