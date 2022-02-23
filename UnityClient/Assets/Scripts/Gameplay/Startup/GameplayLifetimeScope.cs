using UnityEngine;
using UnknownSpace.Gameplay.Input;
using UnknownSpace.Gameplay.View;
using VContainer;
using VContainer.Unity;

namespace UnknownSpace.Gameplay.Startup {
	public sealed class GameplayLifetimeScope : LifetimeScope {
		[SerializeField] PlayerView _playerView;
		[SerializeField] InputProvider _inputProvider;

		private void Reset() {
			_playerView = FindObjectOfType<PlayerView>();
			_inputProvider = FindObjectOfType<InputProvider>();
			autoInjectGameObjects.Add(gameObject);
		}

		protected override void Configure(IContainerBuilder builder) {
			builder.RegisterInstance(_playerView);
			builder.RegisterInstance(_inputProvider);
		}
	}
}
