using Leopotam.Ecs;
using UnityEngine;
using UnknownSpace.Components;
using UnknownSpace.Data;
using UnknownSpace.Meta.Components;
using UnknownSpace.Meta.Data;
using UnknownSpace.Service;

namespace UnknownSpace.Meta.Systems {
	public sealed class MoveTransitionSystem : IEcsRunSystem {
		readonly TimeData _timeData = null;
		readonly PlayerData _playerData = null;
		readonly PlayerStateService _playerStateService = null;
		readonly EcsFilter<Waypoint, WaypointTransition> _filter = null;

		public void Run() {
			foreach ( var idx in _filter ) {
				ref var transition = ref _filter.Get2(idx);
				ref var playerPosition = ref _playerData.Entity.Get<Position>();
				if ( transition.Progress < transition.Timer ) {
					transition.Progress += _timeData.DeltaTime;
					playerPosition.Value = Vector2.Lerp(transition.StartPosition, transition.EndPosition, transition.Progress / transition.Timer);
					_playerData.TransitionCountdown = transition.Timer - transition.Progress;
				} else {
					var playerState = _playerStateService.State;
					playerState.CurrentWaypoint = _filter.Get1(idx).Id;
					_playerStateService.SaveState();
					playerPosition.Value = transition.EndPosition;
					ref var entity = ref _filter.GetEntity(idx);
					entity.Del<WaypointTransition>();
					_playerData.TransitionCountdown = 0;
				}
			}
		}
	}
}