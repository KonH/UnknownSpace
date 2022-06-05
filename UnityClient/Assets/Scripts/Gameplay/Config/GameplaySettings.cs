using UnityEngine;

namespace UnknownSpace.Gameplay.Config {
	[CreateAssetMenu]
	public sealed class GameplaySettings : ScriptableObject {
		[Tooltip("Which directions are available to move")]
		[SerializeField]
		Direction _movementMask = Direction.Horizontal;

		[Tooltip("Actual movement speed")]
		[SerializeField]
		float _movementStep = 0.1f;

		[Tooltip("Maximum available move radius from origin")]
		[SerializeField]
		float _movementArea = 0.5f;

		[Tooltip("Projectile life area from origin")]
		[SerializeField]
		float _projectileArea = 1.0f;

		[Tooltip("Enemy life area from origin")]
		[SerializeField]
		float _enemyArea = 1.0f;

		[Tooltip("Map from entity type to game objects to spawn entity views")]
		[SerializeField]
		EntityTypeGameObjectDictionary _viewFactory;

		[Tooltip("How fast player projectile moves")]
		[SerializeField]
		Vector2 _playerProjectileDirection;

		[Tooltip("Where enemies should spawn")]
		[SerializeField]
		Direction _enemySpawnMask = Direction.Up;

		[Tooltip("How many spawn points should be generated on each direction")]
		[SerializeField]
		int _spawnPointCountPerDirection = 5;

		[Tooltip("Minimal time between spawns on the same spawn point")]
		[SerializeField]
		float _enemyMinSpawnTime = 1.0f;

		[Tooltip("Maximum time between spawns on the same spawn point")]
		[SerializeField]
		float _enemyMaxSpawnTime = 5.0f;

		[Tooltip("How enemies should move")]
		[SerializeField]
		Vector2 _enemyMoveDirection;

		[Tooltip("Minimal time between enemy shoots")]
		[SerializeField]
		float _enemyMinShootInterval = 0.75f;

		[Tooltip("Maximum time between enemy shoots")]
		[SerializeField]
		float _enemyMaxShootInterval = 1.5f;

		[Tooltip("How many scores player gets after killing enemy")]
		[SerializeField]
		int _enemyKillScore = 10;

		[Tooltip("How many health player have on start")]
		[SerializeField]
		int _initialHealth = 10;

		[Tooltip("Max player health")]
		[SerializeField]
		int _maxHealth = 10;

		[Tooltip("How many damage enemy produced by hit")]
		[SerializeField]
		int _enemyHitDamage = 1;

		public Direction MovementMask => _movementMask;
		public float MovementStep => _movementStep;
		public float MovementArea => _movementArea;
		public float ProjectileArea => _projectileArea;
		public float EnemyArea => _enemyArea;
		public EntityTypeGameObjectDictionary ViewFactory => _viewFactory;
		public Vector2 PlayerProjectileDirection => _playerProjectileDirection;
		public Direction EnemySpawnMask => _enemySpawnMask;
		public int SpawnPointCountPerDirection => _spawnPointCountPerDirection;
		public float EnemyMinSpawnTime => _enemyMinSpawnTime;
		public float EnemyMaxSpawnTime => _enemyMaxSpawnTime;
		public Vector2 EnemyMoveDirection => _enemyMoveDirection;
		public float EnemyMinShootInterval => _enemyMinShootInterval;
		public float EnemyMaxShootInterval => _enemyMaxShootInterval;
		public int EnemyKillScore => _enemyKillScore;
		public int InitialHealth => _initialHealth;
		public int MaxHealth => _maxHealth;
		public int EnemyHitDamage => _enemyHitDamage;
	}
}