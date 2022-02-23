using UnityEngine;

namespace UnknownSpace.Gameplay.Config {
	[CreateAssetMenu]
	public sealed class GameplaySettings : ScriptableObject {
		[Tooltip("Which directions are available to move")]
		[SerializeField]
		Direction _movementMask = Direction.Horizontal;

		[Tooltip("Actual movement speed")]
		[SerializeField]
		float _movementStep = 0.1f;

		[Tooltip("Maximum available move radius from origin")]
		[SerializeField]
		float _movementArea = 0.5f;

		[Tooltip("Projectile life area from origin")]
		[SerializeField]
		float _projectileArea = 1.0f;

		public Direction MovementMask => _movementMask;
		public float MovementStep => _movementStep;
		public float MovementArea => _movementArea;
		public float ProjectileArea => _projectileArea;
	}
}