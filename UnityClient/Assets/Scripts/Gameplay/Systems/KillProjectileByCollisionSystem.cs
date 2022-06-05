using Leopotam.Ecs;
using UnknownSpace.Gameplay.Components;

namespace UnknownSpace.Gameplay.Systems {
	/// <summary>
	/// Destroy projectile by CollisionEvent
	/// </summary>
	public sealed class KillProjectileByCollisionSystem : IEcsRunSystem {
		readonly EcsFilter<ProjectileFlag, CollisionEvent> _filter = null;

		public void Run() {
			foreach ( var idx in _filter ) {
				ref var entity = ref _filter.GetEntity(idx);
				entity.Destroy();
			}
		}
	}
}