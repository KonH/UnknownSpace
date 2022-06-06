using UnityEngine;
using UnknownSpace.Meta.Data;
using UnknownSpace.Meta.View;
using UnknownSpace.Meta.Waypoint;
using VContainer;
using VContainer.Unity;

namespace UnknownSpace.Meta.Startup {
	public sealed class MetaLifetimeScope : LifetimeScope {
		[SerializeField] PlayerView _playerView;

		void Reset() {
			_playerView = FindObjectOfType<PlayerView>();
			autoInjectGameObjects.Add(gameObject);
		}

		protected override void Configure(IContainerBuilder builder) {
			builder.RegisterInstance(new PlayerData());
			builder.RegisterInstance(_playerView);
			builder.Register<WaypointProvider>(Lifetime.Scoped);
		}
	}
}