using Leopotam.Ecs;
using UnityEngine;
using UnknownSpace.Gameplay.Components;
using UnknownSpace.Gameplay.Data;

namespace UnknownSpace.Gameplay.Systems {
	public sealed class ReducePlayerHealthByCollisionSystem : IEcsRunSystem {
		readonly int _enemyHitDamage;

		readonly HealthData _healthData = null;
		readonly EcsFilter<PlayerFlag, CollisionEvent> _filter = null;

		public ReducePlayerHealthByCollisionSystem(int enemyHitDamage) {
			_enemyHitDamage = enemyHitDamage;
		}

		public void Run() {
			foreach ( var _ in _filter ) {
				_healthData.CurrentHealth = Mathf.Max(_healthData.CurrentHealth - _enemyHitDamage, 0);
			}
		}
	}
}