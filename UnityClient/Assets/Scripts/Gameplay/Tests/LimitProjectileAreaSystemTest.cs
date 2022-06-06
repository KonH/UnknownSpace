using FluentAssertions;
using Leopotam.Ecs;
using NUnit.Framework;
using UnityEngine;
using UnknownSpace.Components;
using UnknownSpace.Gameplay.Components;
using UnknownSpace.Gameplay.Systems;

namespace UnknownSpace.Tests {
	public sealed class LimitProjectileAreaSystemTest {
		[Test]
		public void IsEntityRemainsInsideArea() {
			var (systems, entity) = InitTestCase(1.5f, Vector2.up);

			systems.Run();

			entity.IsAlive().Should().BeTrue();
		}

		[Test]
		public void IsEntityRemovedOutsideArea() {
			var (systems, entity) = InitTestCase(0.5f, Vector2.up);

			systems.Run();

			entity.IsAlive().Should().BeFalse();
		}

		(EcsSystems, EcsEntity) InitTestCase(float distance, Vector2 pos) {
			var (world, systems) = InitEcs(distance);
			var entity = InitEntity(world, pos);
			return (systems, entity);
		}

		(EcsWorld, EcsSystems) InitEcs(float distance) {
			var world = new EcsWorld();
			var systems = new EcsSystems(world);
			systems
				.Add(new LimitProjectileAreaSystem(distance))
				.Init();
			return (world, systems);
		}

		EcsEntity InitEntity(EcsWorld world, Vector2 pos) {
			var entity = world.NewEntity();
			ref var position = ref entity.Get<Position>();
			position.Value = pos;
			entity.Get<ProjectileFlag>();
			return entity;
		}
	}
}