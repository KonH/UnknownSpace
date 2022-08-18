using Leopotam.Ecs;
using UnityEngine;
using UnknownSpace.Components;
using UnknownSpace.Gameplay.Components;
using UnknownSpace.Gameplay.Data;

namespace UnknownSpace.Gameplay.Systems {
	public sealed class ReducePlayerHealthByCollisionSystem : IEcsRunSystem {
		readonly int _enemyHitDamage;

		readonly HealthData _healthData = null;
		readonly GameData _gameData = null;
		readonly EcsFilter<PlayerFlag, CollisionEvent> _filter = null;

		public ReducePlayerHealthByCollisionSystem(int enemyHitDamage) {
			_enemyHitDamage = enemyHitDamage;
		}

		public void Run() {
			foreach ( var i in _filter ) {
				_healthData.CurrentHealth = Mathf.Max(_healthData.CurrentHealth - _enemyHitDamage, 0);
				if ( (_healthData.CurrentHealth == 0) && !_gameData.IsFinished ) {
					_gameData.IsFinished = true;
					_filter.GetEntity(i).Get<FinishGameEvent>();
				}
			}
		}
	}
}