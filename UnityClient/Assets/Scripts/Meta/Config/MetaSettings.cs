using UnityEngine;
using UnknownSpace.Meta.Service;
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

		[Tooltip("Mapping between Waypoint and GameplaySettings")]
		[SerializeField]
		WaypointIndexGameplaySettingsDictionary _waypointGameplaySettings;

		public float TransitionTime => _transitionTime;

		public WaypointTransitionView WaypointTransitionView => _waypointTransitionView;

		public WaypointIndexGameplaySettingsDictionary WaypointGameplaySettings => _waypointGameplaySettings;
	}
}