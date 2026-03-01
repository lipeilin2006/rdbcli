using RDBCLI.Core;

namespace RDBCLI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ConfigManager.LoadConfig();

            new RDBCLICommand().Parse(args).Invoke();
        }
    }
}
