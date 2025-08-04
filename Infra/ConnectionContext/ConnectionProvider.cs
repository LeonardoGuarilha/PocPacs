using System;
using System.Data;
using Npgsql;

namespace Infra.ConnectionContext;

public class ConnectionProvider : IConnectionProvider, IDisposable
{
    public IDbConnection Connection { get; }

    public IDbTransaction Transaction { get; }

    public ConnectionProvider()
    {
        try
        {
            Connection = CreateConnection();
            Connection.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection("");
    }

    public void Dispose()
    {
        Connection?.Dispose();
    }
}
