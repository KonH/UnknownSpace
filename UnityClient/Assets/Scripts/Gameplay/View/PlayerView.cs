using Leopotam.Ecs;
using UnityEngine;

namespace UnknownSpace.Gameplay.View {
	[RequireComponent(typeof(PositionView))]
	public sealed class PlayerView : MonoBehaviour {
		[SerializeField] PositionView _positionView;

		void Reset() {
			_positionView = GetComponent<PositionView>();
		}

		public void Init(EcsEntity entity) {
			_positionView.Init(entity);
		}
	}
}