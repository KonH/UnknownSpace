using FluentAssertions;
using Leopotam.Ecs;
using NUnit.Framework;
using UnityEngine;
using UnknownSpace.Gameplay.Components;
using UnknownSpace.Gameplay.Data;
using UnknownSpace.Gameplay.Systems;

namespace UnknownSpace.Tests {
	public sealed class MovementSystemTest {
		[Test]
		public void IsPositionChanged() {
			var (systems, entity) = InitTestCase(Vector2.one);

			systems.Run();

			entity.Get<Position>().Value.Should().Be(Vector2.one);
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
				.Inject(new TimeData { DeltaTime = 1 })
				.Add(new MovementSystem(1))
				.Init();
			return (world, systems);
		}

		EcsEntity InitEntity(EcsWorld world, Vector2 direction) {
			var entity = world.NewEntity();
			entity.Get<Position>();
			ref var moveEvent = ref entity.Get<MoveEvent>();
			moveEvent.Direction = direction;
			return entity;
		}
	}
}