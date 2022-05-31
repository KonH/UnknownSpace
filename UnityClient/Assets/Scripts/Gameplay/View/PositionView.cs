using Leopotam.Ecs;
using UnityEngine;
using UnknownSpace.Gameplay.Components;

namespace UnknownSpace.Gameplay.View {
	public sealed class PositionView : MonoBehaviour {
		EcsEntity _entity;
		Rigidbody _rigidbody;

		void Awake() {
			_rigidbody = GetComponent<Rigidbody>();
		}

		public void Init(EcsEntity entity) {
			_entity = entity;
		}

		void Update() {
			if ( _rigidbody ) {
				_rigidbody.MovePosition(_entity.Get<Position>().Value);
			} else {
				transform.position = _entity.Get<Position>().Value;
			}
		}
	}
}