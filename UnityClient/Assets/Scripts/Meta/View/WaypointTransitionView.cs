using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;
using UnknownSpace.Meta.Data;
using UnknownSpace.Meta.Waypoint;
using VContainer;

namespace UnknownSpace.Meta.View {
	public sealed class WaypointTransitionView : MonoBehaviour {
		[SerializeField] Button _button;

		int _waypointId;
		PlayerData _playerData;
		WaypointProvider _provider;

		void Reset() {
			_button = GetComponentInChildren<Button>();
		}

		void Awake() {
			_button.onClick.AddListener(OnClick);
		}

		[Inject]
		public void Init(PlayerData playerData, EcsEntity entity, WaypointProvider provider) {
			_waypointId = entity.Get<Components.Waypoint>().Id;
			_playerData = playerData;
			_provider = provider;
		}

		void Update() {
			var isCurrentWaypoint = _playerData.CurrentWaypoint == _waypointId;
			var shouldBeActive = !isCurrentWaypoint;
			var isActive = _button.gameObject.activeSelf;
			if ( shouldBeActive != isActive ) {
				_button.gameObject.SetActive(shouldBeActive);
			}
		}

		void OnClick() {
			_provider.Click(_waypointId);
		}
	}
}