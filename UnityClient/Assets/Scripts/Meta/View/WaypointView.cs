using System;
using Leopotam.Ecs;
using UnityEngine;
using UnknownSpace.Meta.Waypoint;
using VContainer;

namespace UnknownSpace.Meta.View {
	public sealed class WaypointView : MonoBehaviour {
		[SerializeField]
		int _id;

		WaypointProvider _provider;

		[Inject]
		public void Init(WaypointProvider provider, Func<EcsEntity, WaypointTransitionView> transitionSpawnFactory) {
			_provider = provider;
			_provider.AddWaypoint(_id, transform.localPosition, e => transitionSpawnFactory(e));
		}
	}
}