using Leopotam.Ecs;
using UnityEngine;
using UnknownSpace.Data;
using UnknownSpace.Systems;
using UnknownSpace.Meta.Components;
using UnknownSpace.Meta.Config;
using UnknownSpace.Meta.Data;
using UnknownSpace.Meta.Systems;
using UnknownSpace.Meta.View;
using UnknownSpace.Meta.Waypoint;
using VContainer;

namespace UnknownSpace.Meta.Startup {
	sealed class MetaEcsStartup : MonoBehaviour {
		MetaSettings _settings;
		PlayerData _playerData;
		PlayerState _playerState;
		PlayerView _playerView;
		WaypointProvider _waypointProvider;

		EcsWorld _world;
		EcsSystems _systems;

		[Inject]
		public void Init(
			MetaSettings settings, PlayerData playerData, PlayerState playerState, PlayerView playerView, WaypointProvider waypointProvider) {
			_settings = settings;
			_playerData = playerData;
			_playerState = playerState;
			_playerView = playerView;
			_waypointProvider = waypointProvider;
		}

		void Start() {
			_world = new EcsWorld();
			_systems = new EcsSystems(_world);
#if UNITY_EDITOR
			Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
			Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif
			var waypoints = _waypointProvider.CreateWaypoints(_world);

			var playerEntity = PlayerInitializer.AddToWorld(_world, waypoints, _playerState.CurrentWaypoint);
			_playerView.Init(playerEntity);
			_playerData.Entity = playerEntity;

			_systems
				.Inject(new TimeData())
				.Inject(_playerData)
				.Inject(_playerState)
				.Add(new TimeProviderSystem())
				.Add(new PlayerTransitionSystem(_settings.TransitionTime))
				.Add(new MoveTransitionSystem())
				.OneFrame<WaypointClickEvent>()
				.Init();
		}

		void Update() {
			_systems?.Run();
		}

		void OnDestroy() {
			if ( _systems == null ) {
				return;
			}
			_systems.Destroy();
			_systems = null;
			_world.Destroy();
			_world = null;
		}
	}
}