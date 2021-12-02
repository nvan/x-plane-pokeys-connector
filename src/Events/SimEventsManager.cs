using nvan.PoKeysConnector.Config;
using System.Collections.Generic;
using System.IO;

namespace nvan.PoKeysConnector.Events
{
    public class SimEventsManager
    {
        private List<SimEvent> events = null;

        private JsonReader<List<SimEvent>> jsonReader;
        private JsonWriter<List<SimEvent>> jsonWriter;

        public SimEventsManager(JsonReader<List<SimEvent>> jsonReader, JsonWriter<List<SimEvent>> jsonWriter)
        {
            this.jsonReader = jsonReader;
            this.jsonWriter = jsonWriter;
        }

        private void LoadEvents()
        {
            if (!File.Exists(jsonReader.GetFile()))
            {
                events = new List<SimEvent>();

                SaveEvents();
                return;
            }

            events = jsonReader.ReadFile();
        }

        public void SaveEvents()
        {
            jsonWriter.WriteFile(events);
        }

        public ref List<SimEvent> GetEvents()
        {
            if (events == null)
            {
                LoadEvents();
            }

            return ref events;
        }
    }
}
