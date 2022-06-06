using Leopotam.Ecs;
using UnityEngine;
using UnknownSpace.View;
using VContainer;

namespace UnknownSpace.Gameplay.View {
	[RequireComponent(typeof(PositionView))]
	public sealed class ProjectileView : MonoBehaviour {
		public EcsEntity Entity { get; private set; }

		[SerializeField] PositionView _positionView;

		[Inject]
		public void Init(EcsEntity entity) {
			Entity = entity;
			_positionView.Init(entity);
		}

		void Reset() {
			_positionView = GetComponent<PositionView>();
		}

		void Update() {
			if ( !Entity.IsAlive() ) {
				Destroy(gameObject);
			}
		}
	}
}