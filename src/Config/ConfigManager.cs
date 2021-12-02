using System.IO;

namespace nvan.PoKeysConnector.Config
{
    public class ConfigManager
    {
        private Config config = null;

        private JsonReader<Config> jsonReader;
        private JsonWriter<Config> jsonWriter;

        public ConfigManager(JsonReader<Config> jsonReader, JsonWriter<Config> jsonWriter)
        {
            this.jsonReader = jsonReader;
            this.jsonWriter = jsonWriter;
        }

        private void LoadConfig()
        {
            if (!File.Exists(jsonReader.GetFile()))
            {
                config = new Config
                {
                    autoStart = false,
                    poKeysIp = "",
                    xPlaneIp = "127.0.0.1"
                };

                SaveConfig();
                return;
            }

            config = jsonReader.ReadFile();
        }

        private void SaveConfig()
        {
            jsonWriter.WriteFile(config);
        }

        public ref Config GetConfig()
        {
            if (config == null)
            {
                LoadConfig();
            }

            return ref config;
        }

        public void UpdateConfig(Config config)
        {
            this.config = config;
            SaveConfig();
        }
    }
}