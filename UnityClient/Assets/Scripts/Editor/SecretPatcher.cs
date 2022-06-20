using System.IO;
using System.Text.RegularExpressions;

namespace UnknownSpace.Editor {
	public static class SecretPatcher {
		public static void SetBrainCloudSecret(string secretKey) {
			var targetFilePath = "Assets/Scripts/Config/SecretStorage.cs";
			var regex = new Regex("BrainCloudAppSecret => (\"\")");
			var oldContents = File.ReadAllText(targetFilePath);
			var match = regex.Match(oldContents);
			var index = match.Groups[1].Index;
			var newContents = oldContents.Insert(index + 1, secretKey);
			File.WriteAllText(targetFilePath, newContents);
		}
	}
}