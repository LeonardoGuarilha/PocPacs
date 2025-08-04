using Domain.UnitOfWork;
using Infra.ConnectionContext;

namespace Infra.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly IConnectionProvider _connectionProvider;

    public UnitOfWork(IConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public void BeginTransaction()
    {
        _connectionProvider.Transaction = _connectionProvider.Connection.BeginTransaction();
    }

    public void Commit()
    {
        _connectionProvider.Transaction.Commit();
    }

    public void Rollback()
    {
        _connectionProvider.Transaction.Rollback();
    }

    public void Dispose()
    {
        _connectionProvider.Transaction?.Dispose();
    }
}
