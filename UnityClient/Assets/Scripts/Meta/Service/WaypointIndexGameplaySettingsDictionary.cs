using System;
using UnknownSpace.Gameplay.Config;

namespace UnknownSpace.Meta.Service {
	[Serializable]
	public sealed class WaypointIndexGameplaySettingsDictionary : SerializableDictionary<int, GameplaySettings> {}
}