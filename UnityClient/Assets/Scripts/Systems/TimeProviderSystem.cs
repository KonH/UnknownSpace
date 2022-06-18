using Leopotam.Ecs;
using UnityEngine;
using UnknownSpace.Components;
using UnknownSpace.Data;

namespace UnknownSpace.Systems {
	/// <summary>
	/// Setup delta time for independent of engine usage in systems
	/// </summary>
	public sealed class TimeProviderSystem : IEcsRunSystem {
		readonly TimeData _timeData = null;
		readonly EcsFilter<PauseEvent> _pauseFilter = null;
		readonly EcsFilter<ResumeEvent> _resumeFilter = null;

		public void Run() {
			if ( !_pauseFilter.IsEmpty() ) {
				_timeData.IsPaused = true;
			}
			if ( !_resumeFilter.IsEmpty() ) {
				_timeData.IsPaused = false;
			}
			var deltaTime = !_timeData.IsPaused ? Time.deltaTime : 0.0f;
			_timeData.DeltaTime = deltaTime;
		}
	}
}