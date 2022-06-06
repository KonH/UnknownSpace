using UnityEngine;

namespace UnknownSpace.Meta.Config {
	[CreateAssetMenu]
	public sealed class MetaSettings : ScriptableObject {
		[Tooltip("How many time is required to transit from one waypoint to another")]
		[SerializeField]
		float _transitionTime = 3;

		public float TransitionTime => _transitionTime;
	}
}