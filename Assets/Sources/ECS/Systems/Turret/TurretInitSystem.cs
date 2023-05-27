using Leopotam.EcsLite;
using UnityEngine;

public class TurretInitSystem : IEcsInitSystem
{
	public void Init(IEcsSystems systems)
	{
		EcsWorld world = systems.GetWorld();
		SharedData sharedData = systems.GetShared<SharedData>();

		var staticData = sharedData.StaticData;
		var sceneData = sharedData.SceneData;
		var runtimeData = sharedData.RuntimeData;

		var turretsSpawnPoints = sceneData.EnemyTurretsSpawnPoints;
		var enemyTurretPrefabs = staticData.EnemyTurrets.PrefabsEnemyTurrets;
		var friendlyTurretsPrefabs = staticData.FriendlyTurrets.PrefabsFriendlyTurrets;

		for (int i = 0; i < turretsSpawnPoints.Count; ++i)
		{
			int entityTurret = world.NewEntity();
			EcsPool<TurretComponent> poolTurretComponents = world.GetPool<TurretComponent>();
			ref var turretComponent = ref poolTurretComponents.Add(entityTurret);

			var randomTurretNumber = new System.Random().Next(0, enemyTurretPrefabs.Count);
			var enemyTurretPrefab = enemyTurretPrefabs[randomTurretNumber];

			var turretInstance = UnityEngine.Object.Instantiate(enemyTurretPrefab, 
				turretsSpawnPoints[i].position, 
				Quaternion.identity);
		}
	}
}
