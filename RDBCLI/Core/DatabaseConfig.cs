using RDBCLI.Core.Enums;

namespace RDBCLI.Core
{
    public class DatabaseConfig
    {
        public string Name { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public DatabaseType DatabaseType { get; set; } = DatabaseType.SqlServer;
        public DatabaseConfig()
        {

        }
        public DatabaseConfig(string name, string connectionString, DatabaseType databaseType)
        {
            Name = name;
            ConnectionString = connectionString;
            DatabaseType = databaseType;
        }
    }
}
