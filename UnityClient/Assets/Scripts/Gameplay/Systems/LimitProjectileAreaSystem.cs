using Leopotam.Ecs;
using UnityEngine;
using UnknownSpace.Gameplay.Components;

namespace UnknownSpace.Gameplay.Systems {
	/// <summary>
	/// Delete projectile entities outside of visible area
	/// </summary>
	public sealed class LimitProjectileAreaSystem : IEcsRunSystem {
		readonly float _maxRadius;

		readonly EcsFilter<Position, ProjectileFlag> _filter = null;

		public LimitProjectileAreaSystem(float maxRadius) {
			_maxRadius = maxRadius;
		}

		public void Run() {
			foreach ( var idx in _filter ) {
				var position = _filter.Get1(idx);
				if ( Vector2.Distance(Vector2.zero, position.Value) > _maxRadius ) {
					_filter.GetEntity(idx).Destroy();
				}
			}
		}
	}
}