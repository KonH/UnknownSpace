using Leopotam.Ecs;
using UnityEngine;
using UnknownSpace.Gameplay.Components;

namespace UnknownSpace.Gameplay.View {
	public sealed class PlayerView : MonoBehaviour {
		EcsEntity _entity;

		public void Init(EcsEntity entity) {
			_entity = entity;
		}

		void Update() {
			transform.position = _entity.Get<Position>().Value;
		}
	}
}