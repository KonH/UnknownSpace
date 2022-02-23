using System;
using UnityEngine;

namespace UnknownSpace.Gameplay.Config {
	[Serializable]
	public sealed class EntityTypeGameObjectDictionary : SerializableDictionary<EntityType, GameObject> {}
}