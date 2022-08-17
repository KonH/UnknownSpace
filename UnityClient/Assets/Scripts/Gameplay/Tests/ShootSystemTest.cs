using FluentAssertions;
using Leopotam.Ecs;
using NUnit.Framework;
using UnknownSpace.Config;
using UnknownSpace.Gameplay.Components;
using UnknownSpace.Gameplay.Systems;

namespace UnknownSpace.Tests {
	public sealed class ShootSystemTest {
		[Test]
		public void IsSpawnEventCreated() {
			var (systems, entity) = InitTestCase();

			systems.Run();

			entity.Has<SpawnEvent>().Should().BeTrue();
			entity.Get<SpawnEvent>().Type.Should().Be(EntityType.Projectile);
		}

		(EcsSystems, EcsEntity) InitTestCase() {
			var (world, systems) = InitEcs();
			var entity = InitEntity(world);
			return (systems, entity);
		}

		(EcsWorld, EcsSystems) InitEcs() {
			var world = new EcsWorld();
			var systems = new EcsSystems(world);
			systems
				.Add(new ShootSystem())
				.Init();
			return (world, systems);
		}

		EcsEntity InitEntity(EcsWorld world) {
			var entity = world.NewEntity();
			entity.Get<ShootEvent>();
			return entity;
		}
	}
}