using System;
using UnknownSpace.Gameplay.Config;

namespace UnknownSpace.Service {
	[Serializable]
	public sealed class WaypointIndexGameplaySettingsDictionary : SerializableDictionary<int, GameplaySettings> {}
}