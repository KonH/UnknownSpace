using Leopotam.Ecs;
using UnityEngine;
using UnknownSpace.Components;
using UnknownSpace.Data;
using UnknownSpace.Meta.Components;
using UnknownSpace.Meta.Data;

namespace UnknownSpace.Meta.Systems {
	public sealed class MoveTransitionSystem : IEcsRunSystem {
		readonly TimeData _timeData = null;
		readonly PlayerData _playerData = null;
		readonly EcsFilter<Waypoint, WaypointTransition> _filter = null;

		public void Run() {
			foreach ( var idx in _filter ) {
				ref var transition = ref _filter.Get2(idx);
				ref var playerPosition = ref _playerData.Entity.Get<Position>();
				if ( transition.Progress < transition.Timer ) {
					transition.Progress += _timeData.DeltaTime;
					playerPosition.Value = Vector2.Lerp(transition.StartPosition, transition.EndPosition, transition.Progress / transition.Timer);
				} else {
					_playerData.CurrentWaypoint = _filter.Get1(idx).Id;
					playerPosition.Value = transition.EndPosition;
					ref var entity = ref _filter.GetEntity(idx);
					entity.Del<WaypointTransition>();
				}
			}
		}
	}
}