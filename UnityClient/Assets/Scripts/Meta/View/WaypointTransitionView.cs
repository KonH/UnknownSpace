using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;
using UnknownSpace.Meta.Waypoint;
using UnknownSpace.Service;
using VContainer;

namespace UnknownSpace.Meta.View {
	public sealed class WaypointTransitionView : MonoBehaviour {
		[SerializeField] Button _button;

		int _waypointId;
		PlayerStateService _playerStateService;
		WaypointProvider _provider;

		void Reset() {
			_button = GetComponentInChildren<Button>();
		}

		void Awake() {
			_button.onClick.AddListener(OnClick);
		}

		[Inject]
		public void Init(PlayerStateService playerStateService, EcsEntity entity, WaypointProvider provider) {
			_waypointId = entity.Get<Components.Waypoint>().Id;
			_playerStateService = playerStateService;
			_provider = provider;
		}

		void Update() {
			var playerState = _playerStateService.State;
			var isCurrentWaypoint = playerState.CurrentWaypoint == _waypointId;
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