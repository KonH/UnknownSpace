using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;
using UnknownSpace.Meta.Waypoint;
using VContainer;

namespace UnknownSpace.Meta.View {
	public sealed class WaypointTransitionView : MonoBehaviour {
		[SerializeField] Button _button;

		EcsEntity _entity;
		WaypointProvider _provider;

		void Reset() {
			_button = GetComponentInChildren<Button>();
		}

		void Awake() {
			_button.onClick.AddListener(OnClick);
		}

		[Inject]
		public void Init(EcsEntity entity, WaypointProvider provider) {
			_entity = entity;
			_provider = provider;
		}

		void OnClick() {
			var waypoint = _entity.Get<Components.Waypoint>();
			_provider.Click(waypoint.Id);
		}
	}
}