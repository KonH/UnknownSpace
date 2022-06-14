using System.Collections.Generic;
using Leopotam.Ecs;
using UnknownSpace.Components;

namespace UnknownSpace.Meta.Startup {
	static class PlayerInitializer {
		public static EcsEntity AddToWorld(EcsWorld world, List<EcsEntity> waypoints, int currentWaypoint) {
			var entity = world.NewEntity();
			ref var position = ref entity.Get<Position>();
			foreach ( var waypointEntity in waypoints ) {
				if ( waypointEntity.Get<Components.Waypoint>().Id == currentWaypoint ) {
					position.Value = waypointEntity.Get<Position>().Value;
				}
			}
			entity.Get<PlayerFlag>();
			return entity;
		}
	}
}