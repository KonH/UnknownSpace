using Leopotam.Ecs;
using UnityEngine;
using UnknownSpace.Gameplay.Components;
using UnknownSpace.Gameplay.Data;

namespace UnknownSpace.Gameplay.Systems {
	/// <summary>
	/// Trigger EnemyShootEvent periodically for each enemy
	/// </summary>
	public sealed class EnemyShootUpdateSystem : IEcsRunSystem {
		readonly TimeData _timeData = null;

		readonly EcsFilter<EnemyFlag, EnemyShooter> _filter = null;

		readonly float _minShootInterval;
		readonly float _maxShootInterval;

		public EnemyShootUpdateSystem(float minShootInterval, float maxShootInterval) {
			_minShootInterval = minShootInterval;
			_maxShootInterval = maxShootInterval;
		}

		public void Run() {
			foreach ( var idx in _filter ) {
				ref var entity = ref _filter.GetEntity(idx);
				ref var shooter = ref _filter.Get2(idx);
				if ( shooter.ElapsedTime < shooter.CurrentTimer ) {
					shooter.ElapsedTime += _timeData.DeltaTime;
					continue;
				}
				var isFirstTime = shooter.CurrentTimer <= 0;
				shooter.CurrentTimer = Random.Range(_minShootInterval, _maxShootInterval);
				if ( isFirstTime ) {
					continue;
				}
				shooter.ElapsedTime = 0;
				entity.Get<EnemyShootEvent>();
			}
		}
	}
}