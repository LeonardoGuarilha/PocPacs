using Domain.Entities.Image;
using Domain.Queries;
using Shared.Core.Data;
using Shared.Core.Result;

namespace Domain.Repositories.Read;

public interface IWadoRepository : IRepository<Image>
{
    Task<Result<List<GetMetadata>>> RetrieveMetadata(string studyUid, string seriesUid, string instanceUid, int idUnidade);
    Task<Result<GetImage>> RetrieveSopInstance(string studyUid, string seriesUid, string instanceUid, int idUnidade);
}
