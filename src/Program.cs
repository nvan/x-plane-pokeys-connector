using nvan.PoKeysConnector.Config;
using nvan.PoKeysConnector.Events;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace nvan.PoKeysConnector
{
    static class Program
    {
        private const string configFile = "config.json";
        private const string eventsFile = "events.json";

        [STAThread]
        static void Main()
        {
            var configManager = new ConfigManager(
                new JsonReader<Config.Config>(configFile),
                new JsonWriter<Config.Config>(configFile)
            );

            var simEventsManager = new SimEventsManager(
                new JsonReader<List<SimEvent>>(eventsFile),
                new JsonWriter<List<SimEvent>>(eventsFile)
            );

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new mainForm(ref configManager, ref simEventsManager));
        }
    }
}
