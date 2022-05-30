using Leopotam.Ecs;
using UnityEngine;
using VContainer;

namespace UnknownSpace.Gameplay.View {
	[RequireComponent(typeof(PositionView))]
	public sealed class EnemyView : MonoBehaviour {
		[SerializeField] PositionView _positionView;

		EcsEntity _entity;

		[Inject]
		public void Init(EcsEntity entity) {
			_entity = entity;
			_positionView.Init(entity);
		}

		void Reset() {
			_positionView = GetComponent<PositionView>();
		}

		void Update() {
			if ( !_entity.IsAlive() ) {
				Destroy(gameObject);
			}
		}
	}
}