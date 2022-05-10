using System;
using Leopotam.Ecs;
using UnityEngine;
using UnknownSpace.Gameplay.Components;
using UnknownSpace.Gameplay.Config;

namespace UnknownSpace.Gameplay.Systems {
	/// <summary>
	/// Creates spawn points outside of screen based on enemy spawn mask
	/// </summary>
	public sealed class SetSpawnPointSystem : IEcsInitSystem {
		const float SpawnOffset = 0.05f;
		
		readonly EcsWorld _world = null;
		
		readonly Direction _enemySpawnMask;
		readonly int _spawnPointPerDirection;
		readonly Rect _nonSpawnArea;
		
		public SetSpawnPointSystem(Direction enemySpawnMask, int spawnPointPerDirection, Rect nonSpawnArea) {
			_enemySpawnMask = enemySpawnMask;
			_spawnPointPerDirection = spawnPointPerDirection;
			_nonSpawnArea = nonSpawnArea;
		}
		
		public void Init() {
			TryAddSpawnPoints(Direction.Up, OnHorizontal, _ => _nonSpawnArea.yMax + SpawnOffset);
			TryAddSpawnPoints(Direction.Down, OnHorizontal, _ => _nonSpawnArea.yMin - SpawnOffset);
			TryAddSpawnPoints(Direction.Left, _ => _nonSpawnArea.yMin - SpawnOffset, OnVertical);
			TryAddSpawnPoints(Direction.Right, _ => _nonSpawnArea.yMax + SpawnOffset, OnVertical);
		}

		float OnHorizontal(float i) => OnAxis(_nonSpawnArea.center.x, _nonSpawnArea.width, i);

		float OnVertical(float i) => OnAxis(_nonSpawnArea.center.y, _nonSpawnArea.height, i);

		float OnAxis(float center, float dimension, float i) =>
			SpawnOffset + center - dimension / 2 + i / _spawnPointPerDirection * dimension;

		void TryAddSpawnPoints(Direction flag, Func<float, float> xGenerator, Func<float, float> yGenerator) {
			if ( !_enemySpawnMask.HasFlag(flag) ) {
				return;
			}
			for ( var i = 0; i < _spawnPointPerDirection; i++ ) {
				var x = xGenerator(i);
				var y = yGenerator(i);
				var entity = _world.NewEntity();
				ref var position = ref entity.Get<Position>();
				position.Value = new Vector2(x, y);
				entity.Get<SpawnPoint>();
			}
		}
	}
}