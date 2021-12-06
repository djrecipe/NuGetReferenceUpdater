using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace NuGetReferenceUpdater
{
    public class NuGetReferenceUpdaterSettingsFactory
    {
        public NuGetPackageSetting CreateNuGetPackageSetting(string nuspec_path,
            string package_id, string package_ver, IEnumerable<TargetProjectSetting> target_projects)
        {
            if(target_projects == null)
                target_projects = new List<TargetProjectSetting>();
            NuGetPackageSetting setting = new NuGetPackageSetting();
            setting.NuspecPath = nuspec_path;
            setting.PackageId = package_id;
            setting.PackageVersion = package_ver;
            setting.TargetProjects = new TargetProjectSetting[target_projects.Count()]; 
            return setting;
        }
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
