using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace NuGetReferenceUpdater
{
    public class NuGetReferenceUpdaterSettingsFactory
    {
        public NuGetReferenceUpdaterSettings GetSettings(string path)
        {
            string text = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<NuGetReferenceUpdaterSettings>(text);
        }

        public void SaveSettings(NuGetReferenceUpdaterSettings nuGetReferenceUpdaterSettings, string path)
        {
            string text = JsonConvert.SerializeObject(nuGetReferenceUpdaterSettings);
            File.WriteAllText(path, text);
        }
    }
}
