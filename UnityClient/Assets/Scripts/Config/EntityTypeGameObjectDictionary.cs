using System;
using UnityEngine;

namespace UnknownSpace.Config {
	[Serializable]
	public sealed class EntityTypeGameObjectDictionary : SerializableDictionary<EntityType, GameObject> {}
}