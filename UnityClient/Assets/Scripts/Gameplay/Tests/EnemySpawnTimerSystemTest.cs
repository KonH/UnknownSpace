using FluentAssertions;
using Leopotam.Ecs;
using NUnit.Framework;
using UnknownSpace.Gameplay.Components;
using UnknownSpace.Gameplay.Config;
using UnknownSpace.Gameplay.Data;
using UnknownSpace.Gameplay.Systems;

namespace UnknownSpace.Tests {
	public sealed class EnemySpawnTimerSystemTest {
		[Test]
		public void IsSpawnEventCreated() {
			var timeData = new TimeData {
				DeltaTime = 1.0f
			};
			var (systems, entity) = InitTestCase(timeData);

			systems.Run();

			entity.Has<SpawnEvent>().Should().BeTrue();
			entity.Get<SpawnEvent>().Type.Should().Be(EntityType.Enemy);
		}

		(EcsSystems, EcsEntity) InitTestCase(TimeData timeData) {
			var (world, systems) = InitEcs(timeData);
			var entity = InitEntity(world);
			return (systems, entity);
		}

		(EcsWorld, EcsSystems) InitEcs(TimeData timeData) {
			var world = new EcsWorld();
			var systems = new EcsSystems(world);
			systems
				.Inject(timeData)
				.Add(new EnemySpawnTimerSystem(0.1f, 0.9f))
				.Init();
			return (world, systems);
		}

		EcsEntity InitEntity(EcsWorld world) {
			var entity = world.NewEntity();
			entity.Get<SpawnPoint>();
			return entity;
		}
	}
}