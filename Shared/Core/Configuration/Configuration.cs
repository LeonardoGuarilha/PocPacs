using System;

namespace Shared.Core.Configuration;

public class Configuration
{
    private static readonly Configuration instance = new Configuration();

    public static DatabaseConfiguration Database { get; set; } = new();

    public static Configuration Instance => instance;

    public class DatabaseConfiguration
    {
        public string Provider { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        //public string OracleVersion { get; set; } = string.Empty;
    }
}
