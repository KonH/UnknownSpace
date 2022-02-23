using FluentAssertions;
using Leopotam.Ecs;
using NUnit.Framework;
using UnityEngine;
using UnknownSpace.Gameplay.Components;
using UnknownSpace.Gameplay.Systems;

namespace UnknownSpace.Tests {
	public sealed class SteadyMovementSystemTest {
		[Test]
		public void IsMoveEventApplied() {
			var (systems, entity) = InitTestCase(Vector2.one);

			systems.Run();

			entity.Has<MoveEvent>();
			entity.Get<MoveEvent>().Direction.Should().Be(Vector2.one);
		}

		(EcsSystems, EcsEntity) InitTestCase(Vector2 direction) {
			var (world, systems) = InitEcs();
			var entity = InitEntity(world, direction);
			return (systems, entity);
		}

		(EcsWorld, EcsSystems) InitEcs() {
			var world = new EcsWorld();
			var systems = new EcsSystems(world);
			systems
				.Add(new SteadyMovementSystem())
				.Init();
			return (world, systems);
		}

		EcsEntity InitEntity(EcsWorld world, Vector2 direction) {
			var entity = world.NewEntity();
			entity.Get<Position>();
			ref var movement = ref entity.Get<SteadyMovement>();
			movement.Direction = direction;
			return entity;
		}
	}
}