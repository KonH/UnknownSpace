using Leopotam.Ecs;
using UnknownSpace.Gameplay.Components;
using UnknownSpace.Gameplay.Data;
using UnknownSpace.Service;

namespace UnknownSpace.Gameplay.Systems {
	public sealed class PostScoreOnFinishSystem : IEcsRunSystem {
		readonly EcsFilter<FinishGameEvent> _filter = null;
		readonly ScoresData _scoresData = null;
		readonly BrainCloudService _brainCloudService = null;


		public void Run() {
			if ( _filter.IsEmpty() ) {
				return;
			}
			var scores = _scoresData.Scores;
			_brainCloudService.PostScoreToLeaderboard(scores);
		}
	}
}