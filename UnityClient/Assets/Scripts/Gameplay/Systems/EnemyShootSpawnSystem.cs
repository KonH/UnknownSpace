using Leopotam.Ecs;
using UnknownSpace.Gameplay.Components;
using UnknownSpace.Gameplay.Config;

namespace UnknownSpace.Gameplay.Systems {
	/// <summary>
	/// Trigger SpawnEvent for enemy projectile from EnemyShootEvent
	/// </summary>
	public sealed class EnemyShootSpawnSystem : IEcsRunSystem {
		readonly EcsFilter<EnemyShootEvent> _filter = null;

		public void Run() {
			foreach ( var idx in _filter ) {
				var entity = _filter.GetEntity(idx);
				ref var spawnEvent = ref entity.Get<SpawnEvent>();
				spawnEvent.Type = EntityType.EnemyProjectile;
			}
		}
	}
}