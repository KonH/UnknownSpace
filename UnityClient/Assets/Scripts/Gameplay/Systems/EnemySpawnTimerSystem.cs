using Leopotam.Ecs;
using UnityEngine;
using UnknownSpace.Gameplay.Components;
using UnknownSpace.Gameplay.Config;

namespace UnknownSpace.Gameplay.Systems {
	public sealed class EnemySpawnTimerSystem : IEcsInitSystem, IEcsRunSystem {
		readonly float _minSpawnTime;
		readonly float _maxSpawnTime;

		readonly EcsFilter<SpawnPoint> _filter = null;
		readonly TimeData _timeData = null;

		public EnemySpawnTimerSystem(float minSpawnTime, float maxSpawnTime) {
			_minSpawnTime = minSpawnTime;
			_maxSpawnTime = maxSpawnTime;
		}

		public void Init() {
			foreach ( var idx in _filter ) {
				ref var spawnPoint = ref _filter.Get1(idx);
				UpdateTimer(ref spawnPoint);
			}
		}

		public void Run() {
			foreach ( var idx in _filter ) {
				ref var spawnPoint = ref _filter.Get1(idx);
				spawnPoint.RemainingTime -= _timeData.DeltaTime;
				if ( spawnPoint.RemainingTime > 0 ) {
					continue;
				}
				ref var entity = ref _filter.GetEntity(idx);
				ref var spawnEvent = ref entity.Get<SpawnEvent>();
				spawnEvent.Type = EntityType.Enemy;
				UpdateTimer(ref spawnPoint);
			}
		}

		void UpdateTimer(ref SpawnPoint spawnPoint) {
			spawnPoint.RemainingTime = Random.Range(_minSpawnTime, _maxSpawnTime);
		}
	}
}