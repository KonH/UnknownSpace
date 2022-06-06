using Leopotam.Ecs;
using UnknownSpace.Components;
using UnknownSpace.Gameplay.Components;

namespace UnknownSpace.Gameplay.Systems {
	/// <summary>
	/// Apply movement from SteadyMovement to MoveEvent
	/// </summary>
	public sealed class SteadyMovementSystem : IEcsRunSystem {
		readonly EcsFilter<Position, SteadyMovement> _filter = null;

		public void Run() {
			foreach ( var idx in _filter ) {
				var entity = _filter.GetEntity(idx);
				var movement = _filter.Get2(idx);
				ref var moveEvent = ref entity.Get<MoveEvent>();
				moveEvent.Direction = movement.Direction;
			}
		}
	}
}