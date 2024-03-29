using System;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;
using UnknownSpace.Service;
using UnknownSpace.Components;
using UnknownSpace.Meta.Components;
using UnknownSpace.Meta.Config;

namespace UnknownSpace.Meta.Waypoint {
	/// <summary>
	/// Creates entity for each Waypoint on scene & handle clicks
	/// </summary>
	public sealed class WaypointProvider {
		readonly MetaSettings _settings;
		readonly PlayerStateService _playerStateService;

		readonly List<(int, Vector2, Action<EcsEntity>)> _waypoints = new List<(int, Vector2, Action<EcsEntity>)>();
		readonly Dictionary<int, EcsEntity> _entities = new Dictionary<int, EcsEntity>();

		public WaypointProvider(MetaSettings settings, PlayerStateService playerStateService) {
			_settings = settings;
			_playerStateService = playerStateService;
		}

		public void AddWaypoint(int id, Vector2 position, Action<EcsEntity> callback) {
			_waypoints.Add((id, position, callback));
		}

		public List<EcsEntity> CreateWaypoints(EcsWorld world) {
			var waypoints = new List<EcsEntity>(_waypoints.Count);
			foreach ( var waypoint in _waypoints ) {
				var (id, position, callback) = waypoint;
				var waypointEntity = CreateWaypoint(world, id, position);
				callback(waypointEntity);
				waypoints.Add(waypointEntity);
			}
			return waypoints;
		}

		public int GetTransitionCost(int id) => _settings.WaypointTransitionCost;

		public bool IsAvailable(int id) =>
			_playerStateService.State.ResourceCount >= _settings.WaypointTransitionCost;

		public void Click(int id) {
			if ( _entities.TryGetValue(id, out var entity) ) {
				entity.Get<WaypointClickEvent>();
			}
		}

		EcsEntity CreateWaypoint(EcsWorld world, int id, Vector2 position) {
			var entity = world.NewEntity();
			ref var waypoint = ref entity.Get<Components.Waypoint>();
			waypoint.Id = id;
			ref var positionComponent = ref entity.Get<Position>();
			positionComponent.Value = position;
			_entities[id] = entity;
			return entity;
		}
	}
}