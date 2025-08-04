using System;
using System.Data;

namespace Infra.ConnectionContext;

public interface IConnectionProvider
{
    public IDbConnection Connection { get; }
    public IDbTransaction Transaction { get; set; }
    public IDbConnection CreateConnection();

}
