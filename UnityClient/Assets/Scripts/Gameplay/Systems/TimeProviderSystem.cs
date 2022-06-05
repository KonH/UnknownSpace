using Leopotam.Ecs;
using UnityEngine;

namespace UnknownSpace.Gameplay.Systems {
	/// <summary>
	/// Setup delta time for independent of engine usage in systems
	/// </summary>
	public sealed class TimeProviderSystem : IEcsRunSystem {
		readonly TimeData _timeData = null;

		public void Run() {
			_timeData.DeltaTime = Time.deltaTime;
		}
	}
}