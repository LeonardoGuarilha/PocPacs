using Domain.Entities.Study;
using Domain.ValueObjects;
using Shared.Core.Data;

namespace Domain.Repositories.Write;

public interface InsertImageAsImageKey : IRepository<Study> // Imagem existe sem estudo? Caso sim, o repositório deve ser de Image e não de Study
{
    void SetImageAsImageKey(StudyUidVO studyUid, string imageUid);
}
