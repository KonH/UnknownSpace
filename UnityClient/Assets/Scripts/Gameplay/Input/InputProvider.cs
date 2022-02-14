using UnityEngine;
using UnityEngine.InputSystem;
using UnknownSpace.Presents.Inputs;

namespace UnknownSpace.Gameplay.Input {
	[RequireComponent(typeof(PlayerInput))]
	public sealed class InputProvider : MonoBehaviour, GameplayInputs.IPlayerActions {
		public void OnMove(InputAction.CallbackContext context) {
			Debug.Log($"Move: {context.ReadValue<Vector2>()}");
		}

		public void OnFire(InputAction.CallbackContext context) {
			if ( context.performed ) {
				Debug.Log("Fire");
			}
		}
	}
}