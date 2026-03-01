using ConsoleTables;
using RDBCLI.Core;
using RDBCLI.Core.Enums;
using RDBCLI.Resources;
using System.CommandLine;
using System.Data;

namespace RDBCLI.Commands
{
    internal class ExecuteCommand : Command
    {
        private Option<string> databaseOption = new Option<string>("-d", "--database")
        {
            Description = Descriptions.ExecuteCommnadDatabaseOptionDescription,
            Required = true,
        };
        private Option<string> sqlOption = new Option<string>("-s", "--sql")
        {
            Description = Descriptions.ExecuteCommandSQLOptionDescription
        };
        private Option<string> fileOption = new Option<string>("-f", "--file")
        {
            Description = Descriptions.ExecuteCommandFileOptionDescription
        };
        private Option<string> outputOption = new Option<string>("-o", "--output")
        {
            Description = Descriptions.ExecuteCommandOutputOptionDescription
        };
        public ExecuteCommand() : base("execute", Descriptions.ExecuteCommandDescription)
        {
            Options.Add(databaseOption);
            Options.Add(sqlOption);
            Options.Add(fileOption);
            Options.Add(outputOption);

            SetAction(parseResult =>
            {
                string name = parseResult.GetRequiredValue(databaseOption);
                string? filePath = parseResult.GetValue(fileOption);
                string? sql;
                string? outputPath = parseResult.GetValue(outputOption);

                if (string.IsNullOrEmpty(filePath))
                {
                    sql = parseResult.GetRequiredValue(sqlOption);
                }
                else
                {
                    sql = File.ReadAllText(filePath);
                }

                DatabaseConfig? config = ConfigManager.DatabaseConfigs.FirstOrDefault(c => c.Name == name);
                if (config == null)
                {
                    Console.WriteLine(string.Format(Messages.DatabaseNotFound, name));
                    return;
                }
                Core.Database database = new(config);
                var result = database.ExecuteSql(sql);
                if (!result.IsSuccess)
                {
                    Console.WriteLine(result.ErrorMessage);
                    return;
                }
                if (result.OperationType == SqlOperationType.Query)
                {
                    OutputToConsole(result.QueryResult);
                    OutputToFile(result.QueryResult, outputPath);
                }
                else
                {
                    Console.WriteLine(string.Format(Messages.AffectedRows, result.AffectedRows));
                }
            });
        }
        private void OutputToConsole(DataSet dataSet)
        {
            foreach (DataTable table in dataSet.Tables)
            {
                var consoleTable = new ConsoleTable(table.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToArray());
                foreach (DataRow row in table.Rows)
                {
                    consoleTable.AddRow(row.ItemArray);
                }
                consoleTable.Write();
                Console.WriteLine();
            }
        }

        private void OutputToFile(DataSet dataSet, string? outputPath)
        {
            if (outputPath == null) return;
            for (int i = 0; i < dataSet.Tables.Count; i++)
            {
                string filePath = $"{Path.GetFileNameWithoutExtension(outputPath)}_{i}{Path.GetExtension(outputPath)}";
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    DataTable table = dataSet.Tables[i];
                    // Write column headers
                    writer.WriteLine(string.Join(",", table.Columns.Cast<DataColumn>().Select(c => c.ColumnName)));
                    // Write rows
                    foreach (DataRow row in table.Rows)
                    {
                        writer.WriteLine(string.Join(",", row.ItemArray.Select(item => item.ToString())));
                    }
                }
            }
        }
    }
}
