using Leopotam.Ecs;
using UnityEngine;
using UnknownSpace.View;

namespace UnknownSpace.Meta.View {
	public sealed class PlayerView : MonoBehaviour {
		[SerializeField] PositionView _positionView;

		public void Init(EcsEntity entity) {
			_positionView.Init(entity);
		}
	}
}
