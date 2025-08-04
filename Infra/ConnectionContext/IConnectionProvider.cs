using System;
using System.Data;

namespace Infra.ConnectionContext;

public interface IConnectionProvider
{
    public IDbConnection Connection { get; }
    public IDbTransaction Transaction { get; }
    public IDbConnection CreateConnection();

}
