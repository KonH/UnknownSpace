using System;
using Leopotam.Ecs;
using UnityEngine;
using UnknownSpace.Gameplay.Input;
using UnknownSpace.Gameplay.View;
using UnknownSpace.Gameplay.Components;
using UnknownSpace.Gameplay.Config;
using UnknownSpace.Gameplay.Systems;
using VContainer;

namespace UnknownSpace.Gameplay.Startup {
	sealed class GameEcsStartup : MonoBehaviour {
		GameplaySettings _settings;
		PlayerView _playerView;
		InputProvider _inputProvider;
		Func<EntityType, EcsEntity, GameObject> _spawnFactory;

		EcsWorld _world;
		EcsSystems _systems;

		[Inject]
		public void Init(
			GameplaySettings settings, PlayerView playerView, InputProvider inputProvider,
			Func<EntityType, EcsEntity, GameObject> spawnFactory) {
			_settings = settings;
			_playerView = playerView;
			_inputProvider = inputProvider;
			_spawnFactory = spawnFactory;
		}

		void Start() {
			_world = new EcsWorld();
			_systems = new EcsSystems(_world);
#if UNITY_EDITOR
			Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
			Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif
			var playerEntity = PlayerInitializer.AddToWorld(_world);
			_playerView.Init(playerEntity);
			_inputProvider.Init(playerEntity);

			_systems
				.Inject(new TimeData())
				.Inject(_spawnFactory)
				.Add(new TimeProviderSystem())
				.Add(new SpawnSystem(_settings.PlayerProjectileDirection))
				.Add(new LimitPlayerMovementDirectionSystem(_settings.MovementMask))
				.Add(new LimitPlayerMovementAreaSystem(_settings.MovementArea, _settings.MovementStep))
				.Add(new PlayerMovementSystem())
				.Add(new SteadyMovementSystem())
				.Add(new MovementSystem(_settings.MovementStep))
				.Add(new LimitProjectileAreaSystem(_settings.ProjectileArea))
				.OneFrame<PlayerMoveEvent>()
				.OneFrame<MoveEvent>()
				.OneFrame<SpawnEvent>()
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