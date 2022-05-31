using Leopotam.Ecs;
using UnityEngine;
using UnknownSpace.Gameplay.Components;

namespace UnknownSpace.Gameplay.View {
	public sealed class CollisionProvider : MonoBehaviour {
		EcsEntity _entity;

		public void Init(EcsEntity entity) {
			_entity = entity;
		}

		public void OnTriggerEnter(Collider other) {
			if ( other.TryGetComponent<ProjectileView>(out _) ) {
				_entity.Get<CollisionEvent>();
			}
		}
	}
}