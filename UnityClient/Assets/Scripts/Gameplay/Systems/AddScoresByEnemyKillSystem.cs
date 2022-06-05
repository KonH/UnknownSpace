using Leopotam.Ecs;
using UnknownSpace.Gameplay.Components;
using UnknownSpace.Gameplay.Data;

namespace UnknownSpace.Gameplay.Systems {
	/// <summary>
	/// Add scores before enemy was killed by player
	/// </summary>
	public sealed class AddScoresByEnemyKillSystem : IEcsRunSystem {
		readonly int _killScore;

		readonly ScoresData _scoresData = null;
		readonly EcsFilter<EnemyFlag, CollisionEvent> _filter = null;

		public AddScoresByEnemyKillSystem(int killScore) {
			_killScore = killScore;
		}

		public void Run() {
			foreach ( var _ in _filter ) {
				_scoresData.Scores += _killScore;
			}
		}
	}
}