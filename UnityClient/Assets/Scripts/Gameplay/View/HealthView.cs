using UnityEngine;
using UnityEngine.UI;
using UnknownSpace.Gameplay.Data;
using VContainer;

namespace UnknownSpace.Gameplay.View {
	[RequireComponent(typeof(Slider))]
	public sealed class HealthView : MonoBehaviour {
		[SerializeField] Slider _slider;

		HealthData _data;

		int _lastHealth = -1;

		void Reset() {
			_slider = GetComponent<Slider>();
		}

		[Inject]
		public void Init(HealthData data) {
			_data = data;
		}

		void Update() {
			if ( _data.CurrentHealth == _lastHealth ) {
				return;
			}
			_slider.value = (float) _data.CurrentHealth / _data.MaxHealth;
			_lastHealth = _data.CurrentHealth;
		}
	}
}