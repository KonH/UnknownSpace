using System.Linq;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnknownSpace.Presents.Inputs;
using UnknownSpace.Gameplay.Components;

namespace UnknownSpace.Gameplay.Input {
	[RequireComponent(typeof(PlayerInput))]
	public sealed class InputProvider : MonoBehaviour, GameplayInputs.IPlayerActions {
		InputAction _moveAction;

		EcsEntity _entity;

		void Awake() {
			var input = GetComponent<PlayerInput>();
			_moveAction = input.actions.First(a => a.name == "Move");
		}

		void Update() {
			ref var moveEvent = ref _entity.Get<PlayerMoveEvent>();
			moveEvent.Direction = _moveAction.ReadValue<Vector2>();
		}

		public void Init(EcsEntity entity) {
			_entity = entity;
		}

		public void OnMove(InputAction.CallbackContext context) {
			// Handler is not actually required, we need continuous axis value instead of single time event
		}

		public void OnFire(InputAction.CallbackContext context) {
			if ( context.performed ) {
				_entity.Get<ShootEvent>();
			}
		}

		public void OnPause(InputAction.CallbackContext context) {
			// TODO: replace with proper handler later
			SceneManager.LoadScene(0);
		}
	}
}