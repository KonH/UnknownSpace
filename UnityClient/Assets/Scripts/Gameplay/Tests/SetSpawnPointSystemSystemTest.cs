using System.Linq;
using FluentAssertions;
using Leopotam.Ecs;
using NUnit.Framework;
using UnityEngine;
using UnknownSpace.Gameplay.Components;
using UnknownSpace.Gameplay.Config;
using UnknownSpace.Gameplay.Systems;

namespace UnknownSpace.Tests {
	public sealed class SetSpawnPointSystemSystemTest {
		[Test]
		public void IsSpawnPointInstantiatedOnTopOfNonSpawnArea() {
			var rect = new Rect(0, 0, 5, 5);
			var (world, systems) = InitTestCase(Direction.Up, 3, rect);
			
			systems.Init();

			var entities = new EcsEntity[3];
			world.GetAllEntities(ref entities).Should().Be(3);
			entities
				.Select(e => e.Has<Position>() && e.Has<SpawnPoint>())
				.Should().AllBeEquivalentTo(true, "All elements should be actual spawn points");
			entities
				.Select(e => e.Get<Position>().Value)
				.Select(p => !rect.Contains(p))
				.Should().AllBeEquivalentTo(true, "All elements should have positions outside of non spawn area");
		}
		
		[Test]
		public void IsSpawnPointInstantiatedOnAllDirections() {
			var (world, systems) = InitTestCase(Direction.All, 3, new Rect(0, 0, 5, 5));
			
			systems.Init();

			var entities = new EcsEntity[3 * 4];
			world.GetAllEntities(ref entities).Should().Be(3 * 4);
		}
		
		(EcsWorld, EcsSystems) InitTestCase(Direction enemySpawnMask, int spawnPointPerDirection, Rect nonSpawnArea) {
			var world = new EcsWorld();
			var systems = new EcsSystems(world);
			systems
				.Add(new SetSpawnPointSystem(enemySpawnMask, spawnPointPerDirection, nonSpawnArea));
			return (world, systems);
		}
	}
}