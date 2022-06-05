using Leopotam.Ecs;
using UnknownSpace.Gameplay.Components;
using UnknownSpace.Gameplay.Data;

namespace UnknownSpace.Gameplay.Systems {
	/// <summary>
	/// Apply movement direction from MoveEvent to Position
	/// </summary>
	public sealed class MovementSystem : IEcsRunSystem {
		readonly float _moveStep;

		readonly TimeData _time = null;

		readonly EcsFilter<Position, MoveEvent> _filter = null;

		public MovementSystem(float moveStep) {
			_moveStep = moveStep;
		}

		public void Run() {
			foreach ( var idx in _filter ) {
				ref var position = ref _filter.Get1(idx);
				var moveEvent = _filter.Get2(idx);
				position.Value += MovementLogic.GetMovementDelta(moveEvent.Direction, _moveStep, _time.DeltaTime);
			}
		}
	}
}