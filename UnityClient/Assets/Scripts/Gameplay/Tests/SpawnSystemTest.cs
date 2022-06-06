using FluentAssertions;
using Leopotam.Ecs;
using NUnit.Framework;
using UnityEngine;
using UnknownSpace.Components;
using UnknownSpace.Gameplay.Components;
using UnknownSpace.Gameplay.Config;
using UnknownSpace.Gameplay.Systems;

namespace UnknownSpace.Tests {
	public sealed class SpawnSystemTest {
		[Test]
		public void IsProjectileCreated() {
			var (world, systems) = InitTestCase(Vector2.one, EntityType.Projectile);

			systems.Run();

			var entities = new EcsEntity[2];
			world.GetAllEntities(ref entities).Should().Be(2);
			var entity = entities[1];
			entity.Has<ProjectileFlag>().Should().BeTrue();
			entity.Has<Position>().Should().BeTrue();
			entity.Get<Position>().Value.Should().Be(Vector2.one);
		}

		(EcsWorld, EcsSystems) InitTestCase(Vector2 position, EntityType type) {
			var (world, systems) = InitEcs();
			InitEntity(world, position, type);
			return (world, systems);
		}

		(EcsWorld, EcsSystems) InitEcs() {
			var world = new EcsWorld();
			var systems = new EcsSystems(world);
			systems
				.Add(new SpawnSystem(Vector2.zero, Vector2.zero))
				.Init();
			return (world, systems);
		}

		void InitEntity(EcsWorld world, Vector2 position, EntityType type) {
			var entity = world.NewEntity();
			ref var pos = ref entity.Get<Position>();
			pos.Value = position;
			ref var ev = ref entity.Get<SpawnEvent>();
			ev.Type = type;
		}
	}
}