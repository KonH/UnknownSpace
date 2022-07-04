using Leopotam.Ecs;
using UnknownSpace.Components;
using UnknownSpace.Meta.Components;
using UnknownSpace.Meta.Data;
using UnknownSpace.Service;

namespace UnknownSpace.Meta.Systems {
	public sealed class PlayerTransitionSystem : IEcsRunSystem {
		readonly float _transitionTime;

		readonly PlayerData _playerData = null;
		readonly PlayerStateService _playerStateService = null;
		readonly EcsFilter<Waypoint, Position, WaypointClickEvent> _filter = null;

		public PlayerTransitionSystem(float transitionTime) {
			_transitionTime = transitionTime;
		}

		public void Run() {
			foreach ( var idx in _filter ) {
				ref var waypoint = ref _filter.Get1(idx);
				var playerState = _playerStateService.State;
				if ( waypoint.Id == playerState.CurrentWaypoint ) {
					continue;
				}
				ref var entity = ref _filter.GetEntity(idx);
				ref var transition = ref entity.Get<WaypointTransition>();
				transition.StartPosition = _playerData.Entity.Get<Position>().Value;
				transition.EndPosition = _filter.Get2(idx).Value;
				transition.Progress = 0;
				transition.Timer = _transitionTime;
			}
		}
	}
}