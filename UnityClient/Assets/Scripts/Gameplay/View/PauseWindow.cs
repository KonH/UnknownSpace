using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;
using UnknownSpace.Components;
using UnknownSpace.Data;
using UnknownSpace.Gameplay.Data;
using UnknownSpace.Service;
using VContainer;

namespace UnknownSpace.Gameplay.View {
	public sealed class PauseWindow : MonoBehaviour {
		[SerializeField] GameObject _root;
		[SerializeField] Button _resumeButton;
		[SerializeField] Button _exitButton;

		LevelService _levelService;
		TimeData _timeData;
		PlayerData _playerData;

		[Inject]
		public void Init(LevelService levelService, TimeData timeData, PlayerData playerData) {
			_levelService = levelService;
			_timeData = timeData;
			_playerData = playerData;
		}

		void Awake() {
			_root.SetActive(false);
			_resumeButton.onClick.AddListener(OnResume);
			_exitButton.onClick.AddListener(OnExit);
		}

		void Update() {
			var isShown = _root.activeSelf;
			var shouldBeShown = _timeData.IsPaused;
			if ( isShown != shouldBeShown ) {
				_root.SetActive(shouldBeShown);
			}
		}

		void OnResume() {
			_playerData.Entity.Get<ResumeEvent>();
		}

		void OnExit() {
			_levelService.GoToMeta();
		}
	}
}