using Leopotam.Ecs;
using UnityEngine;
using UnknownSpace.View;
using VContainer;

namespace UnknownSpace.Gameplay.View {
	[RequireComponent(typeof(PositionView))]
	[RequireComponent(typeof(CollisionProvider))]
	public sealed class EnemyView : MonoBehaviour {
		[SerializeField] PositionView _positionView;
		[SerializeField] CollisionProvider _collisionProvider;

		EcsEntity _entity;

		[Inject]
		public void Init(EcsEntity entity) {
			_entity = entity;
			_positionView.Init(entity);
			_collisionProvider.Init(entity);
		}

		void Reset() {
			_positionView = GetComponent<PositionView>();
			_collisionProvider = GetComponent<CollisionProvider>();
		}

		void Update() {
			if ( !_entity.IsAlive() ) {
				Destroy(gameObject);
			}
		}
	}
}