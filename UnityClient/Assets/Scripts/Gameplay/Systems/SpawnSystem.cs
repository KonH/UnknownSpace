using System;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;
using UnknownSpace.Config;
using UnknownSpace.Components;
using UnknownSpace.Gameplay.Components;
using UnknownSpace.Gameplay.Data;

namespace UnknownSpace.Gameplay.Systems {
	/// <summary>
	/// Spawn new objects by SpawnEvent trigger
	/// </summary>
	public sealed class SpawnSystem : IEcsRunSystem {
		readonly EcsWorld _world = null;
		readonly GameData _gameData = null;
		readonly PlayerData _playerData = null;
		readonly Func<EntityType, EcsEntity, GameObject> _factory = null;

		readonly Vector2 _playerProjectileDirection;
		readonly Vector2 _enemyMoveDirection;

		readonly EcsFilter<Position, SpawnEvent> _filter = null;

		readonly Dictionary<EntityType, Action<EcsEntity>> _entityFactory;

		public SpawnSystem(Vector2 playerProjectileDirection, Vector2 enemyMoveDirection) {
			_playerProjectileDirection = playerProjectileDirection;
			_enemyMoveDirection = enemyMoveDirection;
			_entityFactory = new Dictionary<EntityType, Action<EcsEntity>> {
				[EntityType.Projectile] = SpawnPlayerProjectile,
				[EntityType.Enemy] = SpawnEnemy,
				[EntityType.EnemyProjectile] = SpawnEnemyProjectile,
			};
		}

		public void Run() {
			if ( _gameData.IsFinished ) {
				return;
			}
			foreach ( var idx in _filter ) {
				var targetPosition = _filter.Get1(idx);
				var ev = _filter.Get2(idx);
				var entity = _world.NewEntity();
				ref var position = ref entity.Get<Position>();
				position.Value = targetPosition.Value;
				PrepareEntity(ev.Type, entity);
				_factory?.Invoke(ev.Type, entity);
			}
		}

		void PrepareEntity(EntityType type, EcsEntity entity) {
			if ( _entityFactory.TryGetValue(type, out var factory) ) {
				factory(entity);
			}
		}

		void SpawnPlayerProjectile(EcsEntity entity) {
			entity.Get<ProjectileFlag>();
			ref var movement = ref entity.Get<SteadyMovement>();
			movement.Direction = _playerProjectileDirection;
		}

		void SpawnEnemyProjectile(EcsEntity entity) {
			entity.Get<ProjectileFlag>();
			ref var projectilePosition = ref entity.Get<Position>();
			ref var playerPosition = ref _playerData.Entity.Get<Position>();
			ref var movement = ref entity.Get<SteadyMovement>();
			movement.Direction = (playerPosition.Value - projectilePosition.Value).normalized;
		}

		void SpawnEnemy(EcsEntity entity) {
			entity.Get<EnemyFlag>();
			entity.Get<EnemyShooter>();
			ref var movement = ref entity.Get<SteadyMovement>();
			movement.Direction = _enemyMoveDirection;
		}
	}
}