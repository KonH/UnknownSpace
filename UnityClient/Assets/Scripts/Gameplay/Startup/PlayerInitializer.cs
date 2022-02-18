using Leopotam.Ecs;
using UnknownSpace.Gameplay.Components;

namespace UnknownSpace.Gameplay.Startup {
	static class PlayerInitializer {
		public static EcsEntity AddToWorld(EcsWorld world) {
			var entity = world.NewEntity();
			entity.Get<Position>();
			entity.Get<PlayerFlag>();
			return entity;
		}
	}
}