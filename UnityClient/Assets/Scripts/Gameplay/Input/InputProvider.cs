using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.InputSystem;
using UnknownSpace.Presents.Inputs;
using UnknownSpace.Gameplay.Components;

namespace UnknownSpace.Gameplay.Input {
	[RequireComponent(typeof(PlayerInput))]
	public sealed class InputProvider : MonoBehaviour, GameplayInputs.IPlayerActions {
		EcsEntity _entity;

		public void Init(EcsEntity entity) {
			_entity = entity;
		}

		public void OnMove(InputAction.CallbackContext context) {
			ref var moveEvent = ref _entity.Get<PlayerMoveEvent>();
			moveEvent.Direction = context.ReadValue<Vector2>();
		}

		public void OnFire(InputAction.CallbackContext context) {
			if ( context.performed ) {
				Debug.Log("Fire");
			}
		}
	}
}