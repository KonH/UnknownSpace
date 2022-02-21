using Leopotam.Ecs;
using UnityEngine;

namespace UnknownSpace.Gameplay.Systems {
	public sealed class TimeProviderSystem : IEcsRunSystem {
		readonly TimeData _timeData = null;

		public void Run() {
			_timeData.DeltaTime = Time.deltaTime;
		}
	}
}