using UnityEngine;
using UnknownSpace.Gameplay.Systems;

namespace UnknownSpace.Gameplay.Startup {
	[CreateAssetMenu]
	public sealed class GameplaySettings : ScriptableObject {
		[Tooltip("Which directions are available to move")]
		[SerializeField]
		PossibleDirection _movementMask = PossibleDirection.Horizontal;

		[Tooltip("Actual movement speed")]
		[SerializeField]
		float _movementStep = 0.1f;

		[Tooltip("Maximum available move radius from origin")]
		[SerializeField]
		float _movementArea = 0.5f;

		public PossibleDirection MovementMask => _movementMask;
		public float MovementStep => _movementStep;
		public float MovementArea => _movementArea;
	}
}