using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;
using UnknownSpace.Components;
using UnknownSpace.Gameplay.Data;
using VContainer;

namespace UnknownSpace.Gameplay.View {
	[RequireComponent(typeof(Button))]
	public sealed class PauseButton : MonoBehaviour {
		[SerializeField] Button _button;

		PlayerData _playerData;

		[Inject]
		public void Init(PlayerData playerData) {
			_playerData = playerData;
		}

		void Reset() {
			_button = GetComponent<Button>();
		}

		void Awake() {
			_button.onClick.AddListener(OnPause);
		}

		void OnPause() {
			_playerData.Entity.Get<PauseEvent>();
		}
	}
}