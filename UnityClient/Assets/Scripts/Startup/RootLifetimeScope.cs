using UnityEngine;
using UnknownSpace.Meta.Config;
using UnknownSpace.Service;
using VContainer;
using VContainer.Unity;

namespace UnknownSpace.Startup {
	public sealed class RootLifetimeScope : LifetimeScope {
		[SerializeField] MetaSettings _metaSettings;

		protected override void Configure(IContainerBuilder builder) {
			builder.RegisterInstance(_metaSettings);
			builder.RegisterInstance(_metaSettings.WaypointGameplaySettings);
			builder.Register<GameplaySettingsProvider>(Lifetime.Singleton);
			builder.Register(
				resolver => resolver.Resolve<GameplaySettingsProvider>().CurrentSettings,
				Lifetime.Scoped);
		}
	}
}
