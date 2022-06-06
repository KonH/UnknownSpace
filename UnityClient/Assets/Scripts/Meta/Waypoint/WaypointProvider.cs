using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;
using UnknownSpace.Components;
using UnknownSpace.Meta.Components;

namespace UnknownSpace.Meta.Waypoint {
	/// <summary>
	/// Creates entity for each Waypoint on scene & handle clicks
	/// </summary>
	public sealed class WaypointProvider {
		readonly List<(int, Vector2)> _waypoints = new List<(int, Vector2)>();
		readonly Dictionary<int, EcsEntity> _entities = new Dictionary<int, EcsEntity>();

		public void AddWaypoint(int id, Vector2 position) {
			_waypoints.Add((id, position));
		}

		public void CreateWaypoints(EcsWorld world) {
			foreach ( var waypoint in _waypoints ) {
				var (id, position) = waypoint;
				CreateWaypoint(world, id, position);
			}
		}

		public void Click(int id) {
			if ( _entities.TryGetValue(id, out var entity) ) {
				entity.Get<WaypointClickEvent>();
			}
		}

		void CreateWaypoint(EcsWorld world, int id, Vector2 position) {
			var entity = world.NewEntity();
			ref var waypoint = ref entity.Get<Components.Waypoint>();
			waypoint.Id = id;
			ref var positionComponent = ref entity.Get<Position>();
			positionComponent.Value = position;
			_entities[id] = entity;
		}
	}
}