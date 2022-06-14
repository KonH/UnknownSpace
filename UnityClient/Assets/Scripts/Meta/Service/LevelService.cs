using UnityEngine;
using UnityEngine.SceneManagement;
using UnknownSpace.Service;

namespace UnknownSpace.Meta.Service {
	public sealed class LevelService {
		readonly WaypointIndexGameplaySettingsDictionary _waypointGameplaySettings;
		readonly GameplaySettingsProvider _gameplaySettingsProvider;

		public LevelService(WaypointIndexGameplaySettingsDictionary waypointGameplaySettings, GameplaySettingsProvider gameplaySettingsProvider) {
			_waypointGameplaySettings = waypointGameplaySettings;
			_gameplaySettingsProvider = gameplaySettingsProvider;
		}

		public void StartLevel(int waypointIndex) {
			if ( !_waypointGameplaySettings.TryGetValue(waypointIndex, out var gameplaySettings) ) {
				Debug.LogError($"Gameplay settings for waypoint {waypointIndex} is not found");
				return;
			}
			Debug.Log($"Start level with settings: {gameplaySettings.name}");
			_gameplaySettingsProvider.CurrentSettings = gameplaySettings;
			SceneManager.LoadScene(1);
		}
	}
}