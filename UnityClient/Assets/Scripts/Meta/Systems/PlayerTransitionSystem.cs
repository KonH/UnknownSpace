using Leopotam.Ecs;
using UnityEngine;
using UnknownSpace.Components;
using UnknownSpace.Meta.Components;
using UnknownSpace.Meta.Data;
using UnknownSpace.Service;

namespace UnknownSpace.Meta.Systems {
	public sealed class PlayerTransitionSystem : IEcsRunSystem {
		readonly float _transitionTime;
		readonly int _transitionCost;

		readonly PlayerData _playerData = null;
		readonly PlayerStateService _playerStateService = null;
		readonly EcsFilter<Waypoint, Position, WaypointClickEvent> _filter = null;

		public PlayerTransitionSystem(float transitionTime, int transitionCost) {
			_transitionTime = transitionTime;
			_transitionCost = transitionCost;
		}

		public void Run() {
			foreach ( var idx in _filter ) {
				var playerState = _playerStateService.State;
				if ( playerState.ResourceCount < _transitionCost ) {
					Debug.LogErrorFormat(
						"Invalid behaviour: no enough resource for transition! ({0} < {1})",
						playerState.ResourceCount,
						_transitionCost);
					return;
				}
				ref var waypoint = ref _filter.Get1(idx);
				if ( waypoint.Id == playerState.CurrentWaypoint ) {
					continue;
				}
				ref var entity = ref _filter.GetEntity(idx);
				ref var transition = ref entity.Get<WaypointTransition>();
				transition.StartPosition = _playerData.Entity.Get<Position>().Value;
				transition.EndPosition = _filter.Get2(idx).Value;
				transition.Progress = 0;
				transition.Timer = _transitionTime;
				playerState.ResourceCount -= _transitionCost;
				_playerStateService.SaveState();
			}
		}
	}
}