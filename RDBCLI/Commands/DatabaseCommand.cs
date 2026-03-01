using RDBCLI.Resources;
using RDBCLI.Commands.Database;
using System.CommandLine;

namespace RDBCLI.Commands
{
    internal class DatabaseCommand : Command
    {
        public DatabaseCommand() : base("database", Descriptions.DatabaseCommandDescription)
        {
            Subcommands.Add(new AddCommand());
            Subcommands.Add(new DeleteCommand());
            Subcommands.Add(new ListCommand());
        }
    }
}
