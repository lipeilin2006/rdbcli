using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Data.SQLite;
using System.Data.Odbc;
using FirebirdSql.Data.FirebirdClient;
using RDBCLI.Resources;
using RDBCLI.Core.Enums;

namespace RDBCLI.Core
{
    internal class Database
    {
        public DatabaseConfig DatabaseConfig { get; set; }

        private IDbConnection _connection;

        public Database(DatabaseConfig databaseConfig)
        {
            DatabaseConfig = databaseConfig;
            _connection = CreateConnection();
        }

        private IDbConnection CreateConnection()
        {
            if (string.IsNullOrWhiteSpace(DatabaseConfig.ConnectionString))
            {
                throw new ArgumentException("连接字符串不能为空", nameof(DatabaseConfig.ConnectionString));
            }

            return DatabaseConfig.DatabaseType switch
            {
                DatabaseType.SqlServer => new SqlConnection(DatabaseConfig.ConnectionString),
                DatabaseType.MySql => new MySqlConnection(DatabaseConfig.ConnectionString),
                DatabaseType.Sqlite => new SQLiteConnection(DatabaseConfig.ConnectionString),
                DatabaseType.Oracle => new OracleConnection(DatabaseConfig.ConnectionString),
                DatabaseType.PostgreSQL => new NpgsqlConnection(DatabaseConfig.ConnectionString),
                DatabaseType.ODBC => new OdbcConnection(DatabaseConfig.ConnectionString),
                DatabaseType.Firebird => new FbConnection(DatabaseConfig.ConnectionString),
                _ => throw new NotSupportedException(Messages.UnsupportedDatabaseType)
            };
        }

        public SqlExecuteResult ExecuteSql(string sql)
        {
            // 初始化返回结果
            var result = new SqlExecuteResult();

            // 空值校验
            if (string.IsNullOrWhiteSpace(sql))
            {
                result.IsSuccess = false;
                result.ErrorMessage = "SQL语句不能为空";
                return result;
            }

            try
            {
                _connection.Open();
                using IDbCommand command = _connection.CreateCommand();
                command.CommandText = sql;

                var sqlType = GetSqlOperationType(sql);
                result.OperationType = sqlType;

                if (sqlType == SqlOperationType.Query)
                {
                    IDbDataAdapter adapter = CreateDataAdapter(command);
                    adapter.Fill(result.QueryResult);
                    result.AffectedRows = 0;
                    result.IsSuccess = true;
                }
                else
                {
                    // 增删改操作：返回受影响行数
                    result.AffectedRows = command.ExecuteNonQuery();
                    result.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return result;
        }

        private SqlOperationType GetSqlOperationType(string sql)
        {
            sql = sql.Trim().ToLower();

            var queryKeywords = new[] { "select", "show", "desc", "explain", "pragma" };

            // 判断是否以查询关键字开头
            foreach (var keyword in queryKeywords)
            {
                if (sql.StartsWith(keyword))
                {
                    return SqlOperationType.Query;
                }
            }

            // 其余均视为增删改操作
            return SqlOperationType.NonQuery;
        }

        private IDbDataAdapter CreateDataAdapter(IDbCommand command)
        {
            return DatabaseConfig.DatabaseType switch
            {
                DatabaseType.SqlServer => new SqlDataAdapter((SqlCommand)command),
                DatabaseType.MySql => new MySqlDataAdapter((MySqlCommand)command),
                DatabaseType.Sqlite => new SQLiteDataAdapter((SQLiteCommand)command),
                DatabaseType.Oracle => new OracleDataAdapter((OracleCommand)command),
                DatabaseType.PostgreSQL => new NpgsqlDataAdapter((NpgsqlCommand)command),
                DatabaseType.ODBC => new OdbcDataAdapter((OdbcCommand)command),
                DatabaseType.Firebird => new FbDataAdapter((FbCommand)command),
                _ => throw new NotSupportedException(Messages.UnsupportedDatabaseType)
            };
        }

        public void Dispose()
        {
            _connection?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
