using RDBCLI.Core;
using RDBCLI.Resources;
using System.CommandLine;

namespace RDBCLI.Commands.Database
{
    internal class DeleteCommand : Command
    {
        private Option<string> nameOption = new Option<string>("-n", "--name")
        {
            Description = Descriptions.DeleteDatabaseCommandNameOptionDescription,
            Required = true,
        };
        public DeleteCommand() : base("delete", Descriptions.DeleteDatabaseCommandDescription)
        {
            Options.Add(nameOption);

            SetAction(parseResult =>
            {
                string name = parseResult.GetRequiredValue(nameOption);
                ConfigManager.DatabaseConfigs.RemoveAll(config => config.Name == name);
                ConfigManager.SaveConfig();
                Console.WriteLine(Messages.DatabaseDeleted);
            });
        }
    }
}
