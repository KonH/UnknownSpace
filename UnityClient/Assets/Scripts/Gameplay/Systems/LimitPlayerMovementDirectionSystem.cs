using Leopotam.Ecs;
using UnityEngine;
using UnknownSpace.Gameplay.Components;

namespace UnknownSpace.Gameplay.Systems {
	/// <summary>
	/// Skip movement events which set player to position outside of desired area
	/// </summary>
	public sealed class LimitPlayerMovementDirectionSystem : IEcsRunSystem {
		readonly PossibleDirection _direction;

		readonly EcsFilter<Position, PlayerFlag, PlayerMoveEvent> _filter;

		public LimitPlayerMovementDirectionSystem(PossibleDirection direction) {
			_direction = direction;
		}

		public void Run() {
			foreach ( var idx in _filter ) {
				if ( !IsDirectionValid(_filter.Get3(idx).Direction) ) {
					_filter.GetEntity(idx).Del<PlayerMoveEvent>();
				}
			}
		}

		bool IsDirectionValid(Vector2 direction) {
			// TODO: remove performance hit
			if ( direction.x < 0 && !_direction.HasFlag(PossibleDirection.Left) ) {
				return false;
			}
			if ( direction.x > 0 && !_direction.HasFlag(PossibleDirection.Right) ) {
				return false;
			}
			if ( direction.y < 0 && !_direction.HasFlag(PossibleDirection.Down) ) {
				return false;
			}
			if ( direction.y > 0 && !_direction.HasFlag(PossibleDirection.Up) ) {
				return false;
			}
			return true;
		}
	}
}