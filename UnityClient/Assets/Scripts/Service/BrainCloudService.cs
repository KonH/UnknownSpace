using System;
using System.Collections.Generic;
using BrainCloud;
using BrainCloud.Common;
using BrainCloud.LitJson;
using UnityEngine;
using UnknownSpace.Config;
using Object = UnityEngine.Object;

namespace UnknownSpace.Service {
	public sealed class BrainCloudService {
		readonly BrainCloudWrapper _wrapper;

		readonly Dictionary<string, string> _userAttributes = new Dictionary<string, string>();
		readonly Dictionary<string, int> _entityVersions = new Dictionary<string, int>();

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
				success: (_, _) => {
					_wrapper.PlayerStateService.GetAttributes(
						(json, _) => {
							OnGetAttributes(json);
							onSuccess();
						},
						failure: OnFailure(onFailure));
				},
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

		public void ReadUserEntity(string entityType, Action<string> onSuccess, Action<string> onFailure) {
			if ( !_userAttributes.TryGetValue(entityType, out var entityId) ) {
				Debug.Log("ReadUserEntity: entityType is not yet saved, return empty");
				onSuccess(string.Empty);
				return;
			}
			_wrapper.EntityService.GetEntity(
				entityId, (response, _) => {
					var json = JsonMapper.ToObject(response);
					var version = json["data"]["version"];
					_entityVersions[entityId] = int.Parse(version.ToString());
					onSuccess(response);
				}, failure: OnFailure(onFailure));
		}

		public void CreateOrUpdateUserEntity(string entityType, string entityJson, Action<string> onFailure) {
			if ( _userAttributes.TryGetValue(entityType, out var entityId) ) {
				var entityVersion = _entityVersions.GetValueOrDefault(entityId, 1);
				_wrapper.EntityService.UpdateEntity(
					entityId, entityType, entityJson, ACL.ReadOnly().ToJsonString(), entityVersion,
					success: (response, _) => {
						var json = JsonMapper.ToObject(response);
						var version = json["data"]["version"];
						_entityVersions[entityId] = int.Parse(version.ToString());
					},
					failure: OnFailure(onFailure));
			} else {
				_wrapper.EntityService.CreateEntity(
					entityType, entityJson, ACL.ReadOnly().ToJsonString(),
					success: (response, _) => {
						var json = JsonMapper.ToObject(response);
						var newEntityId = json["data"]["entityId"].ToString();
						_userAttributes[entityType] = newEntityId;
						var version = json["data"]["version"];
						_entityVersions[newEntityId] = int.Parse(version.ToString());
						var attributesJson = JsonMapper.ToJson(_userAttributes);
						_wrapper.PlayerStateService.UpdateAttributes(attributesJson, false);
					},
					failure: OnFailure(onFailure));
			}
		}

		void OnGetAttributes(string attributesJson) {
			var json = JsonMapper.ToObject(attributesJson);
			var attributes = json["data"]["attributes"];
			_userAttributes.Clear();
			foreach ( var attributeKey in attributes.Keys ) {
				var attributeValue = attributes[attributeKey];
				Debug.Log($"Found user attribute '{attributeKey}' = '{attributeValue}'");
				_userAttributes[attributeKey] = attributeValue.ToString();
			}
		}

		FailureCallback OnFailure(Action<string> callback) =>
			(status, reasonCode, jsonError, _) =>
				callback($"Error: {status}/{reasonCode}: {jsonError}");
	}
}