using VContainer;
using VContainer.Unity;

namespace UnknownSpace.Meta.Startup {
	public sealed class MenuLifetimeScope : LifetimeScope {
		void Reset() {
			autoInjectGameObjects.Add(gameObject);
		}

		protected override void Configure(IContainerBuilder builder) {
		}
	}
}