using UnityEngine;
using UnknownSpace.Meta.View;

namespace UnknownSpace.Meta.Config {
	[CreateAssetMenu]
	public sealed class MetaSettings : ScriptableObject {
		[Tooltip("How many time is required to transit from one waypoint to another")]
		[SerializeField]
		float _transitionTime = 3;

		[Tooltip("Waypoint transition panel prefab")]
		[SerializeField]
		WaypointTransitionView _waypointTransitionView;

		public float TransitionTime => _transitionTime;

		public WaypointTransitionView WaypointTransitionView => _waypointTransitionView;
	}
}