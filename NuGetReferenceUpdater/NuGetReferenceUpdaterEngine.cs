using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NuGetReferenceUpdater
{
    public class NuGetReferenceUpdaterEngine
    {
        private string CreateNuGetArgs(NuGetPackageSetting setting)
        {
            string args = $"pack {setting.NuspecPath} -version {setting.PackageVersion} -Properties Configuration={setting.BuildConfiguration}";
            return args;
        }
        public void BuildNuGetPackage(string nuget_path, NuGetPackageSetting setting)
        {
            string args = CreateNuGetArgs(setting);
            Process process = new Process();
            process.StartInfo.FileName = nuget_path;
            process.StartInfo.Arguments = args;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = true;
            string output = "";
            process.OutputDataReceived += (sender, e) => { output+=e.Data;};
            process.Start();
            process.WaitForExit();
            if(process.ExitCode != 0)
            {
                throw new Exception($"Unexpected error code ({process.ExitCode}) while building NuGet package {setting.PackageId}: '{output}'");
            }
            return;
        }
    }
}
