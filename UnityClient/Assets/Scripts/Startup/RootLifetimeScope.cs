using UnityEngine;
using UnknownSpace.Gameplay.Config;
using UnknownSpace.Meta.Config;
using VContainer;
using VContainer.Unity;

namespace UnknownSpace.Startup {
	public sealed class RootLifetimeScope : LifetimeScope {
		[SerializeField] MetaSettings _metaSettings;
		[SerializeField] GameplaySettings _gameplaySettings;

		protected override void Configure(IContainerBuilder builder) {
			builder.RegisterInstance(_metaSettings);
			// TODO: register conditionally based on level type
			builder.RegisterInstance(_gameplaySettings);
		}
	}
}
