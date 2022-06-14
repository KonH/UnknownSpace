using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnknownSpace.Meta.Data;
using VContainer;

namespace UnknownSpace.Meta.View {
	public sealed class QuestWindow : MonoBehaviour {
		[SerializeField] GameObject _root;
		[SerializeField] Button _startButton;

		PlayerData _playerData;

		[Inject]
		public void Init(PlayerData playerData) {
			_playerData = playerData;
		}

		void Awake() {
			_startButton.onClick.AddListener(OnStartButtonClick);
		}

		void Update() {
			var isInTransition = _playerData.TransitionCountdown > 0;
			var shouldBeActive = !isInTransition;
			if ( shouldBeActive != _root.gameObject.activeSelf ) {
				_root.gameObject.SetActive(shouldBeActive);
			}
		}

		void OnStartButtonClick() {
			SceneManager.LoadScene(1);
		}
	}
}