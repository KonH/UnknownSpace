using Leopotam.Ecs;
using UnknownSpace.Gameplay.Components;

namespace UnknownSpace.Gameplay.Systems {
	/// <summary>
	/// Read movement direction from PlayerMoveEvent and write it to MoveEvent
	/// </summary>
	public sealed class PlayerMovementSystem : IEcsRunSystem {
		readonly EcsFilter<Position, PlayerFlag, PlayerMoveEvent> _filter;

		public void Run() {
			foreach ( var idx in _filter ) {
				var entity = _filter.GetEntity(idx);
				ref var moveEvent = ref entity.Get<MoveEvent>();
				moveEvent.Direction = _filter.Get3(idx).Direction;
			}
		}
	}
}