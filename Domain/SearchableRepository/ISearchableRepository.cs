using System;
using Shared.Core.DomainObjects;
using Shared.Core.Result;

namespace Domain.SearchableRepository;

public interface ISearchableRepository<TAggregate> where TAggregate : IAggregateRoot
{
    Task<SearchOutput<TAggregate>> Search(SearchInput input, CancellationToken cancellationToken);
    //Task<Result<SearchOutput<TAggregate>>> Search(SearchInput input, CancellationToken cancellationToken);
}
