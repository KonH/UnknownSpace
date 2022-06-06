using Leopotam.Ecs;
using UnityEngine;
using UnknownSpace.Components;

namespace UnknownSpace.View {
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