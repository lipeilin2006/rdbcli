using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace RDBCLI.Core
{
    public class ConfigManager
    {
        public static List<DatabaseConfig> DatabaseConfigs { get; private set; }

        public static void LoadConfig()
        {
            string dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "rdbcli");
            string configPath = Path.Combine(dataPath, "config.yaml");

            if (!Directory.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
            }
            if (!File.Exists(configPath))
            {
                DatabaseConfigs = new List<DatabaseConfig>();
                return;
            }

            var deserializer = new DeserializerBuilder()
                .IgnoreUnmatchedProperties()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            DatabaseConfigs = deserializer.Deserialize<List<DatabaseConfig>>(File.ReadAllText(configPath));
        }

        public static void SaveConfig()
        {
            string dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "rdbcli");
            string configPath = Path.Combine(dataPath, "config.yaml");

            if (!Directory.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
            }

            var serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            File.WriteAllText(configPath, serializer.Serialize(DatabaseConfigs));
        }
    }
}
