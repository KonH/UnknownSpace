using UnityEngine;

namespace UnknownSpace.Gameplay.Camera {
	/// <summary>
	/// Lookup camera rect for 2D world
	/// </summary>
	[RequireComponent(typeof(UnityEngine.Camera))]
	public sealed class CameraRectProvider : MonoBehaviour {
		/// <summary>
		/// Non-efficient for repetitive calls
		/// </summary>
		public Rect Rect {
			get {
				var cam = GetComponent<UnityEngine.Camera>();
				var rect = cam.pixelRect;
				var worldSize = new Vector2(
					GetXWorldPosition(cam, rect.width),
					GetYWorldPosition(cam, rect.height));
				var worldOrigin = new Vector2(
					GetXWorldPosition(cam, rect.center.x),
					GetYWorldPosition(cam, rect.center.y))
					- worldSize / 2;
				var worldRect = new Rect(worldOrigin, worldSize);
				return worldRect;
			}
		}

		float GetXWorldPosition(UnityEngine.Camera cam, float x) =>
			cam.ScreenToWorldPoint(new Vector3(x, 0)).x * 2.0f;
		
		float GetYWorldPosition(UnityEngine.Camera cam, float y) =>
			cam.ScreenToWorldPoint(new Vector3(0, y)).y * 2.0f;
	}
}
