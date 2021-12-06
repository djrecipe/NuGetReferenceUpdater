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
        public string GetTestDir()
        {
            var uri = new Uri(Assembly.GetExecutingAssembly().CodeBase);
            var full_path = Uri.UnescapeDataString(uri.AbsolutePath);
            var dir = Path.GetDirectoryName(full_path);
            return dir;
        }
        [Fact]
        public void UpdateGetPackage()
        {
            // parameters
            string pkg_id = "Test";
            string pkg_ver = "1.0.0";
            string dir = GetTestDir();
            string out_dir = Path.Combine(GetTestDir(), Guid.NewGuid().ToString());
            string pkg_path = Path.Combine(dir, out_dir, $"{pkg_id}.{pkg_ver}.nupkg");
            string nuget_path = Path.Combine(dir, "nuget.exe");
            string nuspec_path = Path.Combine(dir, "Simple.nuspec");
            // create new pkg
            Directory.CreateDirectory(out_dir);
            NuGetPackageSetting setting = new NuGetPackageSetting();
            setting.NuspecPath = nuspec_path;
            setting.PackageId = pkg_id;
            setting.PackageVersion = pkg_ver;
            NuGetReferenceUpdaterEngine engine = new NuGetReferenceUpdaterEngine();
            engine.UpdateNuGetPackage(nuget_path, out_dir, setting);
            // assertion(s)
            bool file_exists = File.Exists(pkg_path);
            Assert.True(file_exists);
        }
        [Fact]
        public void UpdateTargetProject()
        {
            // parameters
            string pkg_id = "Test";
            string pkg_ver = "1.0.0";
            string dir = GetTestDir();
            string nuspec_path = Path.Combine(dir, "Simple.nuspec");
            string csproj_path = Path.Combine(dir, "SimpleTestProject.csproj");
            // define pkg
            NuGetPackageSetting pkg_setting = new NuGetPackageSetting();
            pkg_setting.NuspecPath = nuspec_path;
            pkg_setting.PackageId = pkg_id;
            pkg_setting.PackageVersion = pkg_ver;
            // define proj
            TargetProjectSetting proj_setting = new TargetProjectSetting();
            proj_setting.Path = csproj_path;
            // update csproj
            NuGetReferenceUpdaterEngine engine = new NuGetReferenceUpdaterEngine();
            engine.UpdateTargetProject(pkg_setting, proj_setting);
        }
    }
}
