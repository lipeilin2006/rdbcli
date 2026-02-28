using RDBCLI.Resources;
using RDBCLI.Commands.Database;
using System.CommandLine;
using RDBCLI.Core;

namespace RDBCLI.Commands
{
    internal class DatabaseCommand : ICommandProvider
    {
        public Command ProvideCommand()
        {
            Command command = new("database", Descriptions.DatabaseCommandDescription);
            command.Subcommands.Add(new AddCommand().ProvideCommand());
            command.Subcommands.Add(new DeleteCommand().ProvideCommand());
            command.Subcommands.Add(new ListCommand().ProvideCommand());
            return command;
        }
    }
}
