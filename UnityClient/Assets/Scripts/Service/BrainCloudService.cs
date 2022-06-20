using System;
using BrainCloud;
using UnityEngine;
using UnknownSpace.Config;
using Object = UnityEngine.Object;

namespace UnknownSpace.Service {
	public sealed class BrainCloudService {
		readonly BrainCloudWrapper _wrapper;

		public BrainCloudService(AppConfig appConfig, SecretStorage secretStorage) {
			var go = new GameObject("[BrainCloudWrapper]") {
				hideFlags = HideFlags.DontSave
			};
			Object.DontDestroyOnLoad(go);
			_wrapper = go.AddComponent<BrainCloudWrapper>();
			_wrapper.Init(
				"https://api.braincloudservers.com/dispatcherv2",
				secretStorage.BrainCloudAppSecret, appConfig.BrainCloudAppId,
				Application.version);
			_wrapper.Client.EnableLogging(true);
		}

		public void Login(string email, string password, Action onSuccess, Action<string> onFailure) {
			_wrapper.AuthenticateEmailPassword(
				email, password, forceCreate: false,
				success: (_, _) => onSuccess(),
				failure: OnFailure(onFailure));
		}

		public void Register(string email, string displayName, string password, Action onSuccess, Action<string> onFailure) {
			_wrapper.AuthenticateEmailPassword(
				email, password, forceCreate: true,
				success: (_, _) => {
					_wrapper.PlayerStateService.UpdateName(displayName);
					onSuccess();
				},
				failure: OnFailure(onFailure));
		}

		FailureCallback OnFailure(Action<string> callback) =>
			(status, reasonCode, jsonError, _) =>
				callback($"Error: {status}/{reasonCode}: {jsonError}");
	}
}