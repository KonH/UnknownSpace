using Leopotam.Ecs;

namespace UnknownSpace.Gameplay.Systems {
	public sealed class PlayerData {
		public EcsEntity Entity { get; }

		public PlayerData(EcsEntity entity) {
			Entity = entity;
		}
	}
}