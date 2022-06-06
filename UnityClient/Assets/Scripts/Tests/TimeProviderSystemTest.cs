using FluentAssertions;
using Leopotam.Ecs;
using NUnit.Framework;
using UnityEngine;
using UnknownSpace.Data;
using UnknownSpace.Systems;

namespace UnknownSpace.Tests {
	public sealed class TimeProviderSystemTest {
		[Test]
		public void IsTimeProviderSetDeltaTime() {
			var world = new EcsWorld();
			var systems = new EcsSystems(world);
			var timeData = new TimeData();
			systems
				.Inject(timeData)
				.Add(new TimeProviderSystem())
				.Init();

			systems.Run();

			timeData.DeltaTime.Should().Be(Time.deltaTime);
		}
	}
}