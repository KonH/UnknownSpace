using Leopotam.Ecs;
using UnityEngine;
using UnknownSpace.Service;
using UnknownSpace.Components;
using UnknownSpace.Meta.Config;
using UnknownSpace.Meta.Data;
using UnknownSpace.Meta.View;
using UnknownSpace.Meta.Waypoint;
using VContainer;
using VContainer.Unity;

namespace UnknownSpace.Meta.Startup {
	public sealed class MetaLifetimeScope : LifetimeScope {
		[SerializeField] Camera _camera;
		[SerializeField] PlayerView _playerView;
		[SerializeField] Transform _waypointTransitionRoot;

		void Reset() {
			_playerView = FindObjectOfType<PlayerView>();
			autoInjectGameObjects.Add(gameObject);
		}

		protected override void Configure(IContainerBuilder builder) {
			builder.RegisterInstance(new PlayerData());
			builder.RegisterInstance(_playerView);
			builder.Register<WaypointProvider>(Lifetime.Singleton);
			builder.RegisterFactory<EcsEntity, WaypointTransitionView>(resolver => {
				return e => {
					var settings = resolver.Resolve<MetaSettings>();
					var prefab = settings.WaypointTransitionView;
					var scopedInstaller = resolver.CreateScope(scopeBuilder => {
						scopeBuilder.RegisterInstance(e);
					});
					var instance = scopedInstaller.Instantiate(prefab, _waypointTransitionRoot);
					var position = e.Get<Position>().Value;
					var cameraPosition = _camera.WorldToScreenPoint(position);
					instance.transform.localPosition =
						cameraPosition - new Vector3((float) Screen.width / 2, (float) Screen.height / 2, 0);
					return instance;
				};
			}, Lifetime.Scoped);
		}
	}
}