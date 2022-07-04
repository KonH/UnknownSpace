using UnityEngine;
using UnknownSpace.Gameplay.Config;

namespace UnknownSpace.Service {
	public sealed class GameplaySettingsProvider {
		public GameplaySettings CurrentSettings {
			get {
				var playerState = _playerStateService.State;
				var waypointIndex = playerState.CurrentWaypoint;
				if ( !_waypointGameplaySettings.TryGetValue(waypointIndex, out var gameplaySettings) ) {
					Debug.LogError($"Gameplay settings for waypoint {waypointIndex} is not found");
					return null;
				}
				Debug.Log($"Load level settings: {gameplaySettings.name}");
				return gameplaySettings;
			}
		}

		readonly WaypointIndexGameplaySettingsDictionary _waypointGameplaySettings;
		readonly PlayerStateService _playerStateService;

		public GameplaySettingsProvider(WaypointIndexGameplaySettingsDictionary waypointGameplaySettings, PlayerStateService playerStateService) {
			_waypointGameplaySettings = waypointGameplaySettings;
			_playerStateService = playerStateService;
		}
	}
}