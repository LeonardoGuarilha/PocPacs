using Domain.Entities.Study;
using Domain.Queries;
using Domain.SearchableRepository;
using Shared.Core.Data;
using Shared.Core.Result;

namespace Domain.Repositories.Read;

public interface IStudyRepository : IRepository<Study>, ISearchableRepository<Study>
{
    Task<Result<IEnumerable<GetAllStudies>>> GetAllStudies();
}
