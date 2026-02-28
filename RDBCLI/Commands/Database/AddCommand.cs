using RDBCLI.Core;
using RDBCLI.Resources;
using RDBCLI.Core.Enums;
using System.CommandLine;

namespace RDBCLI.Commands.Database
{
    internal class AddCommand : ICommandProvider
    {
        public Command ProvideCommand()
        {
            Command command = new Command("add", Descriptions.AddDatabaseCommandDescription);

            Option<string> nameOption = new Option<string>("-n", "--name")
            {
                Description = Descriptions.AddDatabaseCommandNameOptionDescription
            };
            Option<string> connectionStringOption = new Option<string>("-c", "--connection-string")
            {
                Description = Descriptions.AddDatabaseCommandConnectionStringOptionDescription
            };
            Option<DatabaseType> typeOption = new Option<DatabaseType>("-t", "--type")
            {
                Description = Descriptions.AddDatabaseCommandTypeOptionDescription
            };
            command.Options.Add(nameOption);
            command.Options.Add(connectionStringOption);
            command.Options.Add(typeOption);

            command.SetAction(parseResult =>
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

            return command;
        }
    }
}
