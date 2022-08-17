using Leopotam.Ecs;
using UnknownSpace.Config;
using UnknownSpace.Gameplay.Components;

namespace UnknownSpace.Gameplay.Systems {
	/// <summary>
	/// Trigger SpawnEvent from ShootEvent
	/// </summary>
	public sealed class ShootSystem : IEcsRunSystem {
		readonly EcsFilter<ShootEvent> _filter = null;

		public void Run() {
			foreach ( var idx in _filter ) {
				var entity = _filter.GetEntity(idx);
				ref var spawnEvent = ref entity.Get<SpawnEvent>();
				spawnEvent.Type = EntityType.Projectile;
			}
		}
	}
}