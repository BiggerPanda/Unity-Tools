using System;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;

namespace JM_Tools.Builder
{
    public class Builder
    {
        private static BuildTarget buildTarget = BuildTarget.StandaloneWindows;
        private static BuildOptions buildOptions = BuildOptions.None;
        private static System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        private static string path = "";

        public static void Build()
        {
            string _gameName = "BuiltGame";
            string[] _levels = EditorBuildSettings.scenes.Where(x => x.enabled).Select(x => x.path).ToArray();
            string[] _args = System.Environment.GetCommandLineArgs();
            string _input = "";


            stopwatch.Start();
            Console.WriteLine("Started Build");
            for (int i = 0; i < _args.Length; i++)
            {
                if (_args[i] == "-buildTarget")
                {
                    if(Enum.TryParse<BuildTarget>(_args[i + 1], true, out var p))
                    {
                        buildTarget = p;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException($"-platform {_args[i + 1]} should match to {string.Join(",", Enum.GetNames(typeof(BuildTarget)))}");
                    }

                    switch (buildTarget)
                    {
                        case BuildTarget.StandaloneWindows:
                            _gameName = "BuiltGame.exe";
                            break;

                        case BuildTarget.StandaloneWindows64:
                            _gameName = "BuiltGame.exe";
                            break;

                        case BuildTarget.XboxOne:
                            EditorUserBuildSettings.xboxBuildSubtarget = XboxBuildSubtarget.Development;
                            EditorUserBuildSettings.xboxOneDeployMethod = XboxOneDeployMethod.Package;
                            _gameName = "BuiltGameXbox";
                            break;

                        case BuildTarget.PS4:
                            EditorUserBuildSettings.ps4BuildSubtarget = PS4BuildSubtarget.Package;
                            EditorUserBuildSettings.ps4HardwareTarget = PS4HardwareTarget.ProAndBase;
                            _gameName = "BuiltGamePS4";

                            break;
                        case BuildTarget.Switch:
                            //set corretc switch settings
                            _gameName = "BuiltGameSwitch";
                            break;
                    }
                }

                if (_args[i] == "-buildPath")
                {
                    path = _args[i + 1];

                    if (path == "")
                    {
                        path = System.IO.Directory.GetCurrentDirectory();
                        path = path + "/" + _gameName;
                    }
                    else
                    {
                        path = path + "/" + _gameName;
                    }
                }

                if (_args[i] == "-buildAddresables") // without this flag, the build will not build the addressables
                {
                    string value = _args[i + 1];
                    if (value == "true")
                    {
                        buildAsstetBundles(true);
                    }
                    else
                    {
                        buildAsstetBundles();
                    }
                }

                if(_args[i] == "-devBuild")
                {
                    string value = _args[i + 1];
                    if (value == "true")
                    {
                        buildOptions = BuildOptions.Development;
                    }
                    else
                    {
                        buildOptions = BuildOptions.None;
                    }
                }
            }

            var optionsBuild = new BuildPlayerOptions
            {
                scenes = _levels,
                target = buildTarget,
                locationPathName = path,
                options = buildOptions
            };

            var buildRepot = BuildPipeline.BuildPlayer(optionsBuild);

            if (buildRepot.summary.result == BuildResult.Succeeded)
            {
                Console.WriteLine("Build succeeded: " + buildRepot.summary.totalSize + " bytes");
            }
            else
            {
                Console.WriteLine("Build failed");
            }

            stopwatch.Stop();
            generateReport(buildRepot);
            Console.WriteLine("Ended Build");
        }

        private static void buildAsstetBundles(bool _clean = false)
        {
            if(_clean)
            {
                AddressableAssetSettings.CleanPlayerContent(AddressableAssetSettingsDefaultObject.Settings.ActivePlayerDataBuilder);
            }

            AddressableAssetSettings.BuildPlayerContent();
        }

        private static void generateReport(BuildReport summary)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Build Raport of " + DateTime.Now + " " + buildTarget);
            sb.AppendLine("Time Elapsed: " + stopwatch.Elapsed);
            sb.AppendLine("Build Path: " + path);
            sb.AppendLine("Build Target: " + buildTarget);
            sb.AppendLine($@"Build {summary.summary.result}, Errors {summary.summary.totalErrors}, Warnings {summary.summary.totalWarnings}
TotalTime {summary.summary.totalTime}, Size {summary.summary.totalSize}
OutputPath {summary.summary.outputPath}");

            System.IO.File.WriteAllText(path + "/BuildRaport.txt", sb.ToString());
        }
    }
}