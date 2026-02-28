using RDBCLI.Core;
using RDBCLI.Resources;
using System.CommandLine;

namespace RDBCLI.Commands.Database
{
    internal class DeleteCommand : ICommandProvider
    {
        public Command ProvideCommand()
        {
            Command command = new("delete", Descriptions.DeleteDatabaseCommandDescription);

            Option<string> nameOption = new Option<string>("-n", "--name")
            {
                Description = Descriptions.DeleteDatabaseCommandNameOptionDescription
            };
            command.Options.Add(nameOption);
            command.SetAction(parseResult =>
            {
                string name = parseResult.GetRequiredValue(nameOption);
                ConfigManager.DatabaseConfigs.RemoveAll(config => config.Name == name);
                ConfigManager.SaveConfig();
                Console.WriteLine(Messages.DatabaseDeleted);
            });

            return command;
        }
    }
}
