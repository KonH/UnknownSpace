using FluentAssertions;
using Leopotam.Ecs;
using NUnit.Framework;
using UnityEngine;
using UnknownSpace.Gameplay.Components;
using UnknownSpace.Gameplay.Systems;

namespace UnknownSpace.Tests {
	public sealed class LimitPlayerMovementAreaSystemTest {
		[Test]
		public void IsMoveEventRemainsInsideArea() {
			var (systems, entity) = InitTestCase(1.5f, Vector2.up);

			systems.Run();

			entity.Has<PlayerMoveEvent>().Should().BeTrue();
		}

		[Test]
		public void IsMoveEventRemovedOutsideArea() {
			var (systems, entity) = InitTestCase(0.5f, Vector2.up);

			systems.Run();

			entity.Has<PlayerMoveEvent>().Should().BeFalse();
		}

		(EcsSystems, EcsEntity) InitTestCase(float distance, Vector2 direction) {
			var (world, systems) = InitEcs(distance);
			var entity = InitPlayer(world, direction);
			return (systems, entity);
		}

		(EcsWorld, EcsSystems) InitEcs(float distance) {
			var world = new EcsWorld();
			var systems = new EcsSystems(world);
			systems
				.Inject(new TimeData { DeltaTime = 1 })
				.Add(new LimitPlayerMovementAreaSystem(distance, 1))
				.Init();
			return (world, systems);
		}

		EcsEntity InitPlayer(EcsWorld world, Vector2 direction) {
			var entity = world.NewEntity();
			entity.Get<Position>();
			entity.Get<PlayerFlag>();
			ref var moveEvent = ref entity.Get<PlayerMoveEvent>();
			moveEvent.Direction = direction;
			return entity;
		}
	}
}