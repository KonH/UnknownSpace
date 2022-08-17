using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Leopotam.Ecs;
using NUnit.Framework;
using UnityEngine;
using UnknownSpace.Config;
using UnknownSpace.Components;
using UnknownSpace.Gameplay.Components;
using UnknownSpace.Gameplay.Systems;

namespace UnknownSpace.Tests {
	/// <summary>
	/// Possibly too paranoid test, but it covers all input combinations, good for 100% cover testing
	/// </summary>
	public class LimitPlayerMovementDirectionSystemTest {
		[Test]
		public void IsValidDirectionRemains() {
			var (systems, entity) = InitTestCase(Direction.Horizontal, Vector2.right);

			systems.Run();

			entity.Has<PlayerMoveEvent>().Should().BeTrue();
		}

		[Test]
		public void IsInvalidDirectionMoveEventRemoved() {
			var (systems, entity) = InitTestCase(Direction.Horizontal, Vector2.up);

			systems.Run();

			entity.Has<PlayerMoveEvent>().Should().BeFalse();
		}

		[TestCaseSource(nameof(GetAllTestCases))]
		public void IsDirectionValidCorrect((Direction, Vector2, bool) input) {
			var (mask, vector, isValid) = input;
			Assert.AreEqual(LimitPlayerMovementDirectionSystem.IsDirectionValid(mask, vector), isValid);
		}

		(EcsSystems, EcsEntity) InitTestCase(Direction direction, Vector2 actualDirection) {
			var (world, systems) = InitEcs(direction);
			var entity = InitPlayer(world, actualDirection);
			return (systems, entity);
		}

		(EcsWorld, EcsSystems) InitEcs(Direction direction) {
			var world = new EcsWorld();
			var systems = new EcsSystems(world);
			systems
				.Add(new LimitPlayerMovementDirectionSystem(direction))
				.Init();
			return (world, systems);
		}

		EcsEntity InitPlayer(EcsWorld world, Vector2 actualDirection) {
			var entity = world.NewEntity();
			entity.Get<Position>();
			entity.Get<PlayerFlag>();
			ref var moveEvent = ref entity.Get<PlayerMoveEvent>();
			moveEvent.Direction = actualDirection;
			return entity;
		}

		static IEnumerable<(Direction, Vector2, bool)> GetAllTestCases() => new[] {
			GetTestCases(Direction.None),
			GetTestCases(Direction.Up, Vector2.up),
			GetTestCases(Direction.Down, Vector2.down),
			GetTestCases(Direction.Left, Vector2.left),
			GetTestCases(Direction.Right, Vector2.right),
			GetTestCases(Direction.Vertical, Vector2.up, Vector2.down),
			GetTestCases(Direction.Horizontal, Vector2.left, Vector2.right),
			GetTestCases(Direction.All, Vector2.up, Vector2.down, Vector2.left, Vector2.right),
		}.SelectMany(cases => cases);

		static IEnumerable<(Direction, Vector2, bool)> GetTestCases(Direction direction, params Vector2[] positive) =>
			new[] {
				Vector2.up, Vector2.down, Vector2.left, Vector2.right,
			}.Select(vec => (direction, vec, positive.Contains(vec)));
	}
}
