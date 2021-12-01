using System;
using Newtonsoft.Json;

namespace NuGetReferenceUpdater
{
    [JsonObject]
    public class NuGetReferenceUpdaterSettings
    {
        [JsonProperty]
        public NuGetPackageSetting[] Packages { get; set; }
    }
    [JsonObject]
    public class NuGetPackageSetting
    {
        [JsonProperty]
        public string NuspecPath { get; set; }
        [JsonProperty]
        public string PackageId { get; set; }
        [JsonProperty]
        public string PackageVersion { get; set; }
        [JsonProperty]
        public TargetProjectSetting[] TargetProjects { get; set; }
    }
    [JsonObject]
    public class TargetProjectSetting
    {
        [JsonProperty]
        public string Path { get; set; }
    }
}
