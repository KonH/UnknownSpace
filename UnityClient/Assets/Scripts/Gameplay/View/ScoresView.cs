using TMPro;
using UnityEngine;
using UnknownSpace.Gameplay.Data;
using VContainer;

namespace UnknownSpace.Gameplay.View {
	[RequireComponent(typeof(TMP_Text))]
	public sealed class ScoresView : MonoBehaviour {
		[SerializeField] TMP_Text _text;

		ScoresData _data;

		int _lastScores = -1;

		void Reset() {
			_text = GetComponent<TMP_Text>();
		}

		[Inject]
		public void Init(ScoresData data) {
			_data = data;
		}

		void Update() {
			if ( _data.Scores == _lastScores ) {
				return;
			}
			_text.text = _data.Scores.ToString();
			_lastScores = _data.Scores;
		}
	}
}