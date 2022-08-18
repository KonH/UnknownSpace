using UnityEngine;
using UnityEngine.UI;
using UnknownSpace.Gameplay.Data;
using UnknownSpace.Service;
using VContainer;

namespace UnknownSpace.Gameplay.View {
	public sealed class FinishWindow : MonoBehaviour {
		[SerializeField] GameObject _root;
		[SerializeField] Button _exitButton;

		LevelService _levelService;
		GameData _gameData;

		[Inject]
		public void Init(LevelService levelService, GameData gameData) {
			_levelService = levelService;
			_gameData = gameData;
		}

		void Awake() {
			_root.SetActive(false);
			_exitButton.onClick.AddListener(OnExit);
		}

		void Update() {
			var isShown = _root.activeSelf;
			var shouldBeShown = _gameData.IsFinished;
			if ( isShown != shouldBeShown ) {
				_root.SetActive(shouldBeShown);
			}
		}

		void OnExit() {
			_levelService.GoToMeta();
		}
	}
}