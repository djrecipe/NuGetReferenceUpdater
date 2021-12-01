using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace NuGetReferenceUpdater
{
    public class SettingsFactory
    {
        public Settings GetSettings(string path)
        {
            string text = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Settings>(text);
        }

        public void SaveSettings(Settings settings, string path)
        {
            string text = JsonConvert.SerializeObject(settings);
            File.WriteAllText(path, text);
        }
    }
}
