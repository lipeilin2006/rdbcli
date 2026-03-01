using System.Data;
using RDBCLI.Core.Enums;

namespace RDBCLI.Core
{
    internal class SqlExecuteResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public DataSet QueryResult { get; set; } = new DataSet();
        public int AffectedRows { get; set; }
        public SqlOperationType OperationType { get; set; }
    }
    
}
