using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace NuGetReferenceUpdater
{
    public class NuGetReferenceUpdaterEngine
    {
        private string CreateNuGetArgs(NuGetPackageSetting setting, string out_dir)
        {
            string args = $"pack {setting.NuspecPath} -NonInteractive -OutputDirectory {out_dir} -version {setting.PackageVersion} -Properties Configuration={setting.BuildConfiguration}";
            return args;
        }
        public void UpdateNuGetPackage(string nuget_path, string repo_path, NuGetPackageSetting setting)
        {
            string args = CreateNuGetArgs(setting, repo_path);
            Process process = new Process();
            process.StartInfo.FileName = nuget_path;
            process.StartInfo.Arguments = args;
            process.StartInfo.RedirectStandardOutput = true;
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
        public void UpdateTargetProject(NuGetPackageSetting package, TargetProjectSetting project)
        {
            if(!File.Exists(project.Path))
                throw new Exception($"Invalid project path '{project.Path}' when updating target project");
            string contents = File.ReadAllText(project.Path);
        }
    }
}
