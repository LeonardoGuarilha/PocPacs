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

public class QidoRSRepository : IQidoRSRepository
{
    private readonly IConnectionProvider _connectionProvider;

    public QidoRSRepository(IConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async Task<Result<List<GetAllStudies>>> GetAllStudies(GetStudiesModel input, int idUnidade)
    {
        StringBuilder stringBuilder = new StringBuilder();
        DynamicParameters dynamicParameters = new DynamicParameters();

        stringBuilder.Append("");

        dynamicParameters.Add("id_unidade", idUnidade);

        if (!String.IsNullOrEmpty(input.AcNumber))
        {
            stringBuilder.Append("");
            dynamicParameters.Add("ac_number", input.AcNumber);
        }

        if (!String.IsNullOrEmpty(input.InitialDate) && !String.IsNullOrEmpty(input.FinalDate))
        {
            stringBuilder.Append("AND BETWEEN TO_DATE(@initial_date, 'YYYY-MM-DD') AND TO_DATE(@final_date, 'YYYY-MM-DD') ");
            dynamicParameters.Add("initial_date", input.InitialDate);
            dynamicParameters.Add("final_date", input.FinalDate);
        }

        if (!String.IsNullOrEmpty(input.StudyDescription))
        {
            stringBuilder.Append("AND UPPER() LIKE '%' || @study_description || '%' ");
            dynamicParameters.Add("study_description", input.StudyDescription.ToUpper());
        }

        if (!String.IsNullOrEmpty(input.PatientId))
        {
            stringBuilder.Append("AND  LIKE '%' || @patient_id || '%' ");
            dynamicParameters.Add("patient_id", input.PatientId);
        }

        if (!String.IsNullOrEmpty(input.Modality))
        {
            stringBuilder.Append("AND = @modality ");
            dynamicParameters.Add("modality", input.Modality);
        }

        if (!String.IsNullOrEmpty(input.PatientBirthdate))
        {
            stringBuilder.Append("AND = @patient_birthdate::DATE ");
            dynamicParameters.Add("patient_birthdate", input.PatientBirthdate);
        }

        if (!string.IsNullOrEmpty(input.PatientName))
        {
            stringBuilder.Append("AND LOWER() LIKE LOWER('%' || @patient_name || '%') ");
            dynamicParameters.Add("patient_name", input.PatientName);
        }

        stringBuilder.Append(" GROUP BY ");
        stringBuilder.Append("");

        using (var connection = _connectionProvider.CreateConnection())
        {
            try
            {
                var result = await connection.QueryAsync<GetAllStudies>(stringBuilder.ToString(), dynamicParameters);
                return Result.Success<List<GetAllStudies>>(result.ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Result.Failure<List<GetAllStudies>>(Error.NoData);
            }
        }
    }

    public Task<SearchOutput<Study>> Search(SearchInput input, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
