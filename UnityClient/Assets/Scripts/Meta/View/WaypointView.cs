using UnityEngine;
using UnknownSpace.Meta.Waypoint;
using VContainer;

namespace UnknownSpace.Meta.View {
	public sealed class WaypointView : MonoBehaviour {
		[SerializeField]
		int _id;

		WaypointProvider _provider;

		[Inject]
		public void Init(WaypointProvider provider) {
			_provider = provider;
			_provider.AddWaypoint(_id, transform.localPosition);
		}

		// TODO: replace with UI element
		[ContextMenu("Move")]
		public void Move() {
			_provider.Click(_id);
		}
	}
}