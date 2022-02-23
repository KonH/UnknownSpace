using System;
using Leopotam.Ecs;
using UnityEngine;
using UnknownSpace.Gameplay.Components;
using UnknownSpace.Gameplay.Config;

namespace UnknownSpace.Gameplay.Systems {
	public sealed class SpawnSystem : IEcsRunSystem {
		readonly EcsWorld _world = null;
		readonly Func<EntityType, EcsEntity, GameObject> _factory = null;

		readonly Vector2 _playerProjectileDirection;

		readonly EcsFilter<Position, SpawnEvent> _filter = null;

		public SpawnSystem(Vector2 playerProjectileDirection) {
			_playerProjectileDirection = playerProjectileDirection;
		}

		public void Run() {
			foreach ( var idx in _filter ) {
				var targetPosition = _filter.Get1(idx);
				var ev = _filter.Get2(idx);
				var entity = _world.NewEntity();
				ref var position = ref entity.Get<Position>();
				position.Value = targetPosition.Value;
				switch ( ev.Type ) {
					case EntityType.Projectile: SpawnProjectile(entity); break;
				}
				_factory?.Invoke(ev.Type, entity);
			}
		}

		void SpawnProjectile(EcsEntity entity) {
			entity.Get<ProjectileFlag>();
			ref var movement = ref entity.Get<SteadyMovement>();
			movement.Direction = _playerProjectileDirection;
		}
	}
}