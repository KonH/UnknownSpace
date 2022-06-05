using Leopotam.Ecs;
using UnityEngine;
using UnknownSpace.Gameplay.Camera;
using UnknownSpace.Gameplay.Components;
using UnknownSpace.Gameplay.Config;
using UnknownSpace.Gameplay.Data;
using UnknownSpace.Gameplay.Input;
using UnknownSpace.Gameplay.View;
using VContainer;
using VContainer.Unity;

namespace UnknownSpace.Gameplay.Startup {
	public sealed class GameplayLifetimeScope : LifetimeScope {
		[SerializeField] PlayerView _playerView;
		[SerializeField] InputProvider _inputProvider;
		[SerializeField] CameraRectProvider _cameraProvider;

		private void Reset() {
			_playerView = FindObjectOfType<PlayerView>();
			_inputProvider = FindObjectOfType<InputProvider>();
			_cameraProvider = FindObjectOfType<CameraRectProvider>();
			autoInjectGameObjects.Add(gameObject);
		}

		protected override void Configure(IContainerBuilder builder) {
			builder.RegisterInstance(_playerView);
			builder.RegisterInstance(_inputProvider);
			builder.RegisterInstance(_cameraProvider);
			builder.RegisterFactory<EntityType, EcsEntity, GameObject>(resolver => {
				return (type, e) => {
					var settings = resolver.Resolve<GameplaySettings>();
					if ( !settings.ViewFactory.TryGetValue(type, out var prefab) ) {
						return null;
					}
					var position = e.Get<Position>().Value;
					var scopedInstaller = resolver.CreateScope(scopeBuilder => { scopeBuilder.RegisterInstance(e); });
					var instance = scopedInstaller.Instantiate(prefab, position, Quaternion.identity);
					return instance;
				};
			}, Lifetime.Scoped);
			builder.RegisterInstance(new ScoresData());
		}
	}
}