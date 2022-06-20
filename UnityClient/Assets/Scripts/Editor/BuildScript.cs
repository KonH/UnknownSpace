using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;

namespace UnityBuilderAction {
	public static class BuildScript {
		static readonly string Eol = Environment.NewLine;

		static readonly string[] Secrets =
			{ "androidKeystorePass", "androidKeyaliasName", "androidKeyaliasPass" };

		public static void Build() {
			// Gather values from args
			Dictionary<string, string> options = GetValidatedOptions();

			// Set version for this build
			PlayerSettings.bundleVersion = options["buildVersion"];
			PlayerSettings.macOS.buildNumber = options["buildVersion"];
			PlayerSettings.Android.bundleVersionCode = int.Parse(options["androidVersionCode"]);

			// Apply build target
			var buildTarget = (BuildTarget)Enum.Parse(typeof(BuildTarget), options["buildTarget"]);
			switch ( buildTarget ) {
				case BuildTarget.Android: {
					EditorUserBuildSettings.buildAppBundle = options["customBuildPath"].EndsWith(".aab");
					if ( options.TryGetValue("androidKeystoreName", out string keystoreName) &&
					     !string.IsNullOrEmpty(keystoreName) ) {
						PlayerSettings.Android.keystoreName = keystoreName;
					}
					if ( options.TryGetValue("androidKeystorePass", out string keystorePass) &&
					     !string.IsNullOrEmpty(keystorePass) ) {
						PlayerSettings.Android.keystorePass = keystorePass;
					}
					if ( options.TryGetValue("androidKeyaliasName", out string keyaliasName) &&
					     !string.IsNullOrEmpty(keyaliasName) ) {
						PlayerSettings.Android.keyaliasName = keyaliasName;
					}
					if ( options.TryGetValue("androidKeyaliasPass", out string keyaliasPass) &&
					     !string.IsNullOrEmpty(keyaliasPass) ) {
						PlayerSettings.Android.keyaliasPass = keyaliasPass;
					}
					break;
				}
				case BuildTarget.StandaloneOSX:
					PlayerSettings.SetScriptingBackend(BuildTargetGroup.Standalone, ScriptingImplementation.Mono2x);
					break;
			}

			// Custom build
			Build(buildTarget, options["customBuildPath"]);
		}

		static Dictionary<string, string> GetValidatedOptions() {
			ParseCommandLineArguments(out var validatedOptions);

			if ( !validatedOptions.TryGetValue("projectPath", out _) ) {
				Console.WriteLine("Missing argument -projectPath");
				EditorApplication.Exit(110);
			}

			if ( !validatedOptions.TryGetValue("buildTarget", out var buildTarget) ) {
				Console.WriteLine("Missing argument -buildTarget");
				EditorApplication.Exit(120);
			}

			if ( !Enum.IsDefined(typeof(BuildTarget), buildTarget ?? string.Empty) ) {
				EditorApplication.Exit(121);
			}

			if ( !validatedOptions.TryGetValue("customBuildPath", out _) ) {
				Console.WriteLine("Missing argument -customBuildPath");
				EditorApplication.Exit(130);
			}

			const string defaultCustomBuildName = "TestBuild";
			if ( !validatedOptions.TryGetValue("customBuildName", out var customBuildName) ) {
				Console.WriteLine($"Missing argument -customBuildName, defaulting to {defaultCustomBuildName}.");
				validatedOptions.Add("customBuildName", defaultCustomBuildName);
			} else if ( customBuildName == "" ) {
				Console.WriteLine($"Invalid argument -customBuildName, defaulting to {defaultCustomBuildName}.");
				validatedOptions.Add("customBuildName", defaultCustomBuildName);
			}

			return validatedOptions;
		}

		static void ParseCommandLineArguments(out Dictionary<string, string> providedArguments) {
			providedArguments = new Dictionary<string, string>();
			var args = Environment.GetCommandLineArgs();

			Console.WriteLine(
				$"{Eol}" +
				$"###########################{Eol}" +
				$"#    Parsing settings     #{Eol}" +
				$"###########################{Eol}" +
				$"{Eol}"
			);

			// Extract flags with optional values
			for ( int current = 0, next = 1; current < args.Length; current++, next++ ) {
				// Parse flag
				var isFlag = args[current].StartsWith("-");
				if ( !isFlag ) {
					continue;
				}
				var flag = args[current].TrimStart('-');

				// Parse optional value
				var flagHasValue = next < args.Length && !args[next].StartsWith("-");
				var value = flagHasValue ? args[next].TrimStart('-') : "";
				var secret = Secrets.Contains(flag);
				var displayValue = secret ? "*HIDDEN*" : "\"" + value + "\"";

				// Assign
				Console.WriteLine($"Found flag \"{flag}\" with value {displayValue}.");
				providedArguments.Add(flag, value);
			}
		}

		static void Build(BuildTarget buildTarget, string filePath) {
			var scenes = EditorBuildSettings.scenes.Where(scene => scene.enabled).Select(s => s.path).ToArray();
			var buildPlayerOptions = new BuildPlayerOptions {
				scenes = scenes,
				target = buildTarget,
				locationPathName = filePath,
			};

			BuildSummary buildSummary = BuildPipeline.BuildPlayer(buildPlayerOptions).summary;
			ReportSummary(buildSummary);
			ExitWithResult(buildSummary.result);
		}

		static void ReportSummary(BuildSummary summary) {
			Console.WriteLine(
				$"{Eol}" +
				$"###########################{Eol}" +
				$"#      Build results      #{Eol}" +
				$"###########################{Eol}" +
				$"{Eol}" +
				$"Duration: {summary.totalTime.ToString()}{Eol}" +
				$"Warnings: {summary.totalWarnings.ToString()}{Eol}" +
				$"Errors: {summary.totalErrors.ToString()}{Eol}" +
				$"Size: {summary.totalSize.ToString()} bytes{Eol}" +
				$"{Eol}"
			);
		}

		static void ExitWithResult(BuildResult result) {
			switch ( result ) {
				case BuildResult.Succeeded:
					Console.WriteLine("Build succeeded!");
					EditorApplication.Exit(0);
					break;
				case BuildResult.Failed:
					Console.WriteLine("Build failed!");
					EditorApplication.Exit(101);
					break;
				case BuildResult.Cancelled:
					Console.WriteLine("Build cancelled!");
					EditorApplication.Exit(102);
					break;
				case BuildResult.Unknown:
				default:
					Console.WriteLine("Build result is unknown!");
					EditorApplication.Exit(103);
					break;
			}
		}
	}
}