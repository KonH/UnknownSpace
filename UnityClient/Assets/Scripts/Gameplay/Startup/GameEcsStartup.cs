using System;
using Leopotam.Ecs;
using UnityEngine;
using UnknownSpace.Gameplay.Camera;
using UnknownSpace.Gameplay.Input;
using UnknownSpace.Gameplay.View;
using UnknownSpace.Gameplay.Components;
using UnknownSpace.Gameplay.Config;
using UnknownSpace.Gameplay.Data;
using UnknownSpace.Gameplay.Systems;
using VContainer;

namespace UnknownSpace.Gameplay.Startup {
	sealed class GameEcsStartup : MonoBehaviour {
		GameplaySettings _settings;
		PlayerView _playerView;
		InputProvider _inputProvider;
		CameraRectProvider _cameraProvider;
		Func<EntityType, EcsEntity, GameObject> _spawnFactory;
		ScoresData _scoresData;

		EcsWorld _world;
		EcsSystems _systems;

		[Inject]
		public void Init(
			GameplaySettings settings, PlayerView playerView, InputProvider inputProvider, CameraRectProvider cameraProvider,
			Func<EntityType, EcsEntity, GameObject> spawnFactory, ScoresData scoresData) {
			_settings = settings;
			_playerView = playerView;
			_inputProvider = inputProvider;
			_cameraProvider = cameraProvider;
			_spawnFactory = spawnFactory;
			_scoresData = scoresData;
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
				.Inject(new PlayerData(playerEntity))
				.Inject(_scoresData)
				.Inject(_spawnFactory)
				.Add(new SetSpawnPointSystem(_settings.EnemySpawnMask, _settings.SpawnPointCountPerDirection, _cameraProvider.Rect))
				.Add(new TimeProviderSystem())
				.Add(new EnemySpawnTimerSystem(_settings.EnemyMinSpawnTime, _settings.EnemyMaxSpawnTime))
				.Add(new ShootSystem())
				.Add(new EnemyShootUpdateSystem(_settings.EnemyMinShootInterval, _settings.EnemyMaxShootInterval))
				.Add(new EnemyShootSpawnSystem())
				.Add(new SpawnSystem(_settings.PlayerProjectileDirection, _settings.EnemyMoveDirection))
				.Add(new LimitPlayerMovementDirectionSystem(_settings.MovementMask))
				.Add(new LimitPlayerMovementAreaSystem(_settings.MovementArea, _settings.MovementStep))
				.Add(new PlayerMovementSystem())
				.Add(new SteadyMovementSystem())
				.Add(new MovementSystem(_settings.MovementStep))
				.Add(new LimitProjectileAreaSystem(_settings.ProjectileArea))
				.Add(new AddScoresByEnemyKillSystem(_settings.EnemyKillScore))
				.Add(new KillEnemyByCollisionSystem())
				.Add(new KillProjectileByCollisionSystem())
				.Add(new LimitEnemyAreaSystem(_settings.EnemyArea))
				.OneFrame<PlayerMoveEvent>()
				.OneFrame<MoveEvent>()
				.OneFrame<SpawnEvent>()
				.OneFrame<ShootEvent>()
				.OneFrame<EnemyShootEvent>()
				.OneFrame<CollisionEvent>()
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