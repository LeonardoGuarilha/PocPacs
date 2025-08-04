using System;
using System.Data;
using Npgsql;
using Shared.Core.Configuration;

namespace Infra.ConnectionContext;

public class ConnectionProvider : IConnectionProvider, IDisposable
{
    public IDbConnection Connection { get; }

    public IDbTransaction Transaction { get; set; }

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
        return new NpgsqlConnection(Configuration.Database.ConnectionString);
    }

    public void Dispose()
    {
        Connection?.Dispose();
    }
}
