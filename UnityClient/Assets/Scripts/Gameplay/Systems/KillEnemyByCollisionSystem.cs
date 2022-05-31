using Leopotam.Ecs;
using UnknownSpace.Gameplay.Components;

namespace UnknownSpace.Gameplay.Systems {
	public sealed class KillEnemyByCollisionSystem : IEcsRunSystem {
		readonly EcsFilter<EnemyFlag, CollisionEvent> _filter = null;

		public void Run() {
			foreach ( var idx in _filter ) {
				ref var entity = ref _filter.GetEntity(idx);
				entity.Destroy();
			}
		}
	}
}