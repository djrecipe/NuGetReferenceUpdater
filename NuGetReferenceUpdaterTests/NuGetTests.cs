using NuGetReferenceUpdater;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Xunit;

namespace NuGetReferenceUpdaterTests
{
    public class NuGetTests
    {
        public string GetNuGetPath()
        {
            var uri = new Uri(Assembly.GetExecutingAssembly().CodeBase);
            var full_path = Uri.UnescapeDataString(uri.AbsolutePath);
            var dir = Path.GetDirectoryName(full_path);
            string path = Path.Combine(dir, "nuget.exe");
            Debug.WriteLine($"nuget.exe path '{path}'");
            return path;
        }
        [Fact]
        public void BuildNuGetPackage()
        {
            NuGetReferenceUpdaterEngine engine = new NuGetReferenceUpdaterEngine();
            NuGetPackageSetting setting = new NuGetPackageSetting();
            engine.BuildNuGetPackage(GetNuGetPath(), setting);
        }
    }
}
