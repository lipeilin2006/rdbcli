using System.CommandLine;

namespace RDBCLI.Core
{
    public interface ICommandProvider
    {
        Command ProvideCommand();
    }
}
