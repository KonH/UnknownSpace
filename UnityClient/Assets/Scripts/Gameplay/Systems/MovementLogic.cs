using UnityEngine;

namespace UnknownSpace.Gameplay.Systems {
	/// <summary>
	/// Shared movement logic
	/// </summary>
	public static class MovementLogic {
		public static Vector2 GetMovementDelta(Vector2 direction, float moveStep, float deltaTime) =>
			direction * moveStep * deltaTime;
	}
}