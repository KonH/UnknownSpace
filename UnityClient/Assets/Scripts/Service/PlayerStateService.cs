using System;
using BrainCloud.LitJson;
using UnityEngine;
using UnknownSpace.Data;

namespace UnknownSpace.Service {
	public sealed class PlayerStateService {
		const string EntityType = "PlayerState";

		public PlayerState State { get; private set; } = new PlayerState();

		readonly BrainCloudService _brainCloudService;

		public PlayerStateService(BrainCloudService brainCloudService) {
			_brainCloudService = brainCloudService;
		}

		public void LoadState(Action onSuccess, Action<string> onFailure) {
			_brainCloudService.ReadUserEntity(EntityType,
				json => {
					OnLoadEntity(json);
					onSuccess();
				}, onFailure);
		}

		public void SaveState() {
			var entityJson = JsonMapper.ToJson(State);
			_brainCloudService.CreateOrUpdateUserEntity(EntityType, entityJson, Debug.LogError);
		}

		void OnLoadEntity(string json) {
			if ( string.IsNullOrEmpty(json) ) {
				return;
			}
			var container = JsonMapper.ToObject<DataOf<DataOf<PlayerState>>>(json);
			State = container.data.data;
		}
	}
}