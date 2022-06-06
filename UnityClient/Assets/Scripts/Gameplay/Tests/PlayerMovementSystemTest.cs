using FluentAssertions;
using Leopotam.Ecs;
using NUnit.Framework;
using UnityEngine;
using UnknownSpace.Components;
using UnknownSpace.Gameplay.Components;
using UnknownSpace.Gameplay.Data;
using UnknownSpace.Gameplay.Systems;

namespace UnknownSpace.Tests {
	public sealed class PlayerMovementSystemTest {
		[Test]
		public void IsPlayerMoveEventTranslatedToMoveEvent() {
			var (systems, entity) = InitTestCase(Vector2.up);

			systems.Run();

			entity.Has<MoveEvent>().Should().BeTrue();
			entity.Get<MoveEvent>().Direction.Should().Be(Vector2.up);
		}

		(EcsSystems, EcsEntity) InitTestCase(Vector2 direction) {
			var (world, systems) = InitEcs();
			var entity = InitPlayer(world, direction);
			return (systems, entity);
		}

		(EcsWorld, EcsSystems) InitEcs() {
			var world = new EcsWorld();
			var systems = new EcsSystems(world);
			systems
				.Inject(new GameData())
				.Add(new PlayerMovementSystem())
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