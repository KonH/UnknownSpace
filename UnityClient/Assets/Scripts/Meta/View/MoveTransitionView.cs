using TMPro;
using UnityEngine;
using UnknownSpace.Meta.Data;
using VContainer;

namespace UnknownSpace.Meta.View {
	[RequireComponent(typeof(TMP_Text))]
	public sealed class MoveTransitionView : MonoBehaviour {
		[SerializeField] TMP_Text _text;

		PlayerData _playerData;

		int _lastTimerValue;

		void Reset() {
			_text = GetComponent<TMP_Text>();
		}

		[Inject]
		public void Init(PlayerData playerData) {
			_playerData = playerData;
		}

		void Update() {
			var newTimerValue = Mathf.RoundToInt(_playerData.TransitionCountdown);
			if ( _lastTimerValue == newTimerValue ) {
				return;
			}
			_text.text = (newTimerValue > 0) ? newTimerValue.ToString() : string.Empty;
			_lastTimerValue = newTimerValue;
		}
	}
}