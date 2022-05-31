using Leopotam.Ecs;
using UnityEngine;

namespace UnknownSpace.Gameplay.View {
	[RequireComponent(typeof(PositionView))]
	[RequireComponent(typeof(CollisionProvider))]
	public sealed class PlayerView : MonoBehaviour {
		[SerializeField] PositionView _positionView;
		[SerializeField] CollisionProvider _collisionProvider;

		void Reset() {
			_positionView = GetComponent<PositionView>();
			_collisionProvider = GetComponent<CollisionProvider>();
		}

		public void Init(EcsEntity entity) {
			_positionView.Init(entity);
			_collisionProvider.Init(entity);
		}
	}
}