using Shared.Core.DomainObjects;

namespace Shared.Core.Data;

public interface IRepository<T> : IDisposable where T : IAggregateRoot
{

}
