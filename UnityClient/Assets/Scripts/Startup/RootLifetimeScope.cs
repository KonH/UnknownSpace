using UnityEngine;
using UnknownSpace.Gameplay.Config;
using VContainer;
using VContainer.Unity;

namespace UnknownSpace.Startup {
	public sealed class RootLifetimeScope : LifetimeScope {
		[SerializeField] GameplaySettings _gameplaySettings;

		protected override void Configure(IContainerBuilder builder) {
			// TODO: register conditionally based on level type
			builder.RegisterInstance(_gameplaySettings);
		}
	}
}
