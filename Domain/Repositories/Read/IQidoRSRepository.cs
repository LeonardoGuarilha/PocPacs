using Domain.Entities.Study;
using Domain.Model;
using Domain.Queries;
using Domain.SearchableRepository;
using Shared.Core.Data;
using Shared.Core.Result;

namespace Domain.Repositories.Read;

public interface IQidoRSRepository : IRepository<Study>, ISearchableRepository<Study>
{
    Task<Result<List<GetAllStudies>>> GetAllStudies(GetStudiesModel input, int idUnidade);
}
