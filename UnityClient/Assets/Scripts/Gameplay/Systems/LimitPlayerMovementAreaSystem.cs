using Leopotam.Ecs;
using UnityEngine;
using UnknownSpace.Gameplay.Components;

namespace UnknownSpace.Gameplay.Systems {
	/// <summary>
	/// Skip movement events which set player to position outside of desired area
	/// </summary>
	public sealed class LimitPlayerMovementAreaSystem : IEcsRunSystem {
		readonly float _maxRadius;
		readonly float _moveStep;

		readonly EcsFilter<Position, PlayerFlag, PlayerMoveEvent> _filter;

		public LimitPlayerMovementAreaSystem(float maxRadius, float moveStep) {
			_maxRadius = maxRadius;
			_moveStep = moveStep;
		}

		public void Run() {
			foreach ( var idx in _filter ) {
				var position = _filter.Get1(idx);
				var targetPosition = position.Value + _filter.Get3(idx).Direction * _moveStep;
				if ( Vector2.Distance(Vector2.zero, targetPosition) > _maxRadius ) {
					_filter.GetEntity(idx).Del<PlayerMoveEvent>();
				}
			}
		}
	}
}