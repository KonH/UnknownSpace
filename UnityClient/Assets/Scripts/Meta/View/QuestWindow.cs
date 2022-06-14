using UnityEngine;
using UnityEngine.UI;
using UnknownSpace.Meta.Data;
using UnknownSpace.Meta.Service;
using VContainer;

namespace UnknownSpace.Meta.View {
	public sealed class QuestWindow : MonoBehaviour {
		[SerializeField] GameObject _root;
		[SerializeField] Button _startButton;

		PlayerData _playerData;
		LevelService _levelService;

		[Inject]
		public void Init(PlayerData playerData, LevelService levelService) {
			_playerData = playerData;
			_levelService = levelService;
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
			_levelService.StartLevel();
		}
	}
}