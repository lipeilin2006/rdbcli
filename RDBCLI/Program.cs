using RDBCLI.Core;
using RDBCLI.Resources;
using RDBCLI.Commands;
using System.CommandLine;

namespace RDBCLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConfigManager.LoadConfig();

            RootCommand rootCommand = new("rdbcli")
            {
                Description = Descriptions.RDBCLICommandDescription
            };
            rootCommand.Subcommands.Add(new DatabaseCommand().ProvideCommand());
            rootCommand.Subcommands.Add(new ExecuteCommand().ProvideCommand());

            ParseResult parseResult = rootCommand.Parse(args);
            parseResult.Invoke();
        }
    }
}
