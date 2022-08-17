using TMPro;
using UnityEngine;
using UnknownSpace.Service;
using VContainer;

namespace UnknownSpace.Meta.View {
	[RequireComponent(typeof(TMP_Text))]
	public sealed class CurrentResourceCountView : MonoBehaviour {
		[SerializeField] TMP_Text _text;

		PlayerStateService _playerStateService;

		int _lastValue;

		void Reset() {
			_text = GetComponent<TMP_Text>();
		}

		[Inject]
		public void Init(PlayerStateService playerStateService) {
			_playerStateService = playerStateService;
		}

		void Update() {
			var newValue = _playerStateService.State.ResourceCount;
			if ( _lastValue == newValue ) {
				return;
			}
			_text.text = newValue.ToString();
			_lastValue = newValue;
		}
	}
}