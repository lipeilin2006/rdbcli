using ConsoleTables;
using RDBCLI.Core;
using RDBCLI.Resources;
using System.CommandLine;

namespace RDBCLI.Commands.Database
{
    internal class ListCommand : Command
    {
        public ListCommand() : base("list", Descriptions.ListDatabaseCommandDescription)
        {
            SetAction(parseResult =>
            {
                var table = new ConsoleTable(
                    Descriptions.ListDatabaseTableNameTitle,
                    Descriptions.ListDatabaseTableTypeTitle,
                    Descriptions.ListDatabaseTableConnectionStringTitle);

                foreach (var config in ConfigManager.DatabaseConfigs)
                {
                    table.AddRow(config.Name, config.DatabaseType, config.ConnectionString);
                }
                table.Write();
                Console.WriteLine();
            });
        }
    }
}
