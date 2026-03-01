using RDBCLI.Commands;
using RDBCLI.Resources;
using System.CommandLine;

namespace RDBCLI
{
    internal class RDBCLICommand: RootCommand
    {
        public RDBCLICommand() : base(Descriptions.RDBCLICommandDescription)
        {
            Subcommands.Add(new DatabaseCommand());
            Subcommands.Add(new ExecuteCommand());
        }
    }
}
