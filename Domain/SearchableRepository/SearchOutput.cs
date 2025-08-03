using System;
using Shared.Core.DomainObjects;

namespace Domain.SearchableRepository;

public class SearchOutput<TAggregate> where TAggregate : IAggregateRoot
{
    public int CurrentPage { get; set; }
    public int PerPage { get; set; }
    public IReadOnlyList<TAggregate> Items { get; set; }
    public int Total { get; set; }

    public SearchOutput(int currentPage, int perPage, int total, IReadOnlyList<TAggregate> items)
    {
        CurrentPage = currentPage;
        PerPage = perPage;
        Total = total;
        Items = items;
    }
}