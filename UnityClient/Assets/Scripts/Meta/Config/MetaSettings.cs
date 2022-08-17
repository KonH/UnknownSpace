using UnityEngine;
using UnknownSpace.Config;

namespace UnknownSpace.Meta.Config {
	[CreateAssetMenu]
	public sealed class MetaSettings : ScriptableObject {
		[Tooltip("How many time is required to transit from one waypoint to another")]
		[SerializeField]
		float _transitionTime = 3;

		[Tooltip("Mapping between Waypoint and GameplaySettings")]
		[SerializeField]
		WaypointIndexGameplaySettingsDictionary _waypointGameplaySettings;

		[Tooltip("How expensive transition between waypoints")]
		[SerializeField]
		int _waypointTransitionCost = 5;

		public float TransitionTime => _transitionTime;

		public WaypointIndexGameplaySettingsDictionary WaypointGameplaySettings => _waypointGameplaySettings;

		public int WaypointTransitionCost => _waypointTransitionCost;
	}
}