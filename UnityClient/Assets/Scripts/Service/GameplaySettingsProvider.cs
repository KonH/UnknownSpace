using UnityEngine;
using UnknownSpace.Data;
using UnknownSpace.Gameplay.Config;

namespace UnknownSpace.Service {
	public sealed class GameplaySettingsProvider {
		public GameplaySettings CurrentSettings {
			get {
				var waypointIndex = _playerState.CurrentWaypoint;
				if ( !_waypointGameplaySettings.TryGetValue(waypointIndex, out var gameplaySettings) ) {
					Debug.LogError($"Gameplay settings for waypoint {waypointIndex} is not found");
					return null;
				}
				Debug.Log($"Load level settings: {gameplaySettings.name}");
				return gameplaySettings;
			}
		}

		readonly WaypointIndexGameplaySettingsDictionary _waypointGameplaySettings;
		readonly PlayerState _playerState;

		public GameplaySettingsProvider(WaypointIndexGameplaySettingsDictionary waypointGameplaySettings, PlayerState playerState) {
			_waypointGameplaySettings = waypointGameplaySettings;
			_playerState = playerState;
		}
	}
}