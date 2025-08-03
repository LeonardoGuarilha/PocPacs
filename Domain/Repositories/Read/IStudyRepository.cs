using Domain.Entities.Study;
using Domain.Queries;
using Shared.Core.Data;
using Shared.Core.Result;

namespace Domain.Repositories.Read;

public interface IStudyRepository : IRepository<Study>
{
    Task<Result<IEnumerable<GetAllStudies>>> GetAllStudies();
}
