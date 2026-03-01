using RDBCLI.Core;
using RDBCLI.Resources;
using RDBCLI.Core.Enums;
using System.CommandLine;

namespace RDBCLI.Commands.Database
{
    internal class AddCommand : Command
    {
        private Option<string> nameOption = new Option<string>("-n", "--name")
        {
            Description = Descriptions.AddDatabaseCommandNameOptionDescription,
            Required = true,
        };
        private Option<string> connectionStringOption = new Option<string>("-c", "--connection-string")
        {
            Description = Descriptions.AddDatabaseCommandConnectionStringOptionDescription,
            Required = true,
        };
        private Option<DatabaseType> typeOption = new Option<DatabaseType>("-t", "--type")
        {
            Description = Descriptions.AddDatabaseCommandTypeOptionDescription,
            Required = true,
        };
        public AddCommand() : base("add", Descriptions.AddDatabaseCommandDescription)
        {
            Options.Add(nameOption);
            Options.Add(connectionStringOption);
            Options.Add(typeOption);

            SetAction(parseResult =>
            {
                string name = parseResult.GetRequiredValue(nameOption);
                string connectionString = parseResult.GetRequiredValue(connectionStringOption);
                DatabaseType type = parseResult.GetRequiredValue(typeOption);

                if (ConfigManager.DatabaseConfigs.Any(config => config.Name == name))
                {
                    Console.WriteLine(string.Format(Messages.DatabaseExists, name));
                    return;
                }
                ConfigManager.DatabaseConfigs.Add(new DatabaseConfig(name, connectionString, type));
                ConfigManager.SaveConfig();
                Console.WriteLine(Messages.DatabaseAdded);
            });
        }
    }
}
