using Leopotam.Ecs;
using UnityEngine;
using UnknownSpace.Gameplay.Input;
using UnknownSpace.Gameplay.View;
using UnknownSpace.Gameplay.Components;
using UnknownSpace.Gameplay.Systems;

namespace UnknownSpace.Gameplay.Startup {
	sealed class GameEcsStartup : MonoBehaviour {
		// TODO: prototype approach, replace later (Dependency Injection)
		[SerializeField] PlayerView _playerView;
		// TODO: prototype approach, replace later (Dependency Injection)
		[SerializeField] InputProvider _inputProvider;

		EcsWorld _world;
		EcsSystems _systems;

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
				.Add(new LimitPlayerMovementDirectionSystem(PossibleDirection.Horizontal)) // TODO: prototype approach, replace later (based on gameplay mode)
				.Add(new LimitPlayerMovementAreaSystem(0.5f, 0.1f)) // TODO: prototype approach, replace later (settings)
				.Add(new PlayerMovementSystem())
				.Add(new MovementSystem(0.1f)) // TODO: prototype approach, replace later (settings)
				.OneFrame<PlayerMoveEvent>()
				.OneFrame<MoveEvent>()
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