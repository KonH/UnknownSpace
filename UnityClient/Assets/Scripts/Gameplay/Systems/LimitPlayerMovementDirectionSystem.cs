using Leopotam.Ecs;
using UnityEngine;
using UnknownSpace.Gameplay.Components;
using UnknownSpace.Gameplay.Config;

namespace UnknownSpace.Gameplay.Systems {
	/// <summary>
	/// Skip movement events which set player to position outside of desired area
	/// </summary>
	public sealed class LimitPlayerMovementDirectionSystem : IEcsRunSystem {
		readonly Direction _direction;

		readonly EcsFilter<Position, PlayerFlag, PlayerMoveEvent> _filter = null;

		public LimitPlayerMovementDirectionSystem(Direction direction) {
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
			return IsDirectionValid(_direction, direction);
		}

		public static bool IsDirectionValid(Direction mask, Vector2 direction) {
			if ( direction.x < 0 && !mask.HasFlag(Direction.Left) ) {
				return false;
			}
			if ( direction.x > 0 && !mask.HasFlag(Direction.Right) ) {
				return false;
			}
			if ( direction.y < 0 && !mask.HasFlag(Direction.Down) ) {
				return false;
			}
			if ( direction.y > 0 && !mask.HasFlag(Direction.Up) ) {
				return false;
			}
			return true;
		}
	}
}