using Leopotam.EcsLite;
using UnityEngine;
public class EnemyInitSystem : IEcsInitSystem
{
	public void Init(IEcsSystems systems)
	{
		EcsWorld world = systems.GetWorld();
		int entityEnemy = world.NewEntity();
		SharedData sharedData = systems.GetShared<SharedData>();

		var staticData = sharedData.StaticData;
		var sceneData = sharedData.SceneData;
		var runtimeData = sharedData.RuntimeData;

		EcsPool<CharacterComponent> poolCharacterComponents = world.GetPool<CharacterComponent>();

		ref var characterComponent = ref poolCharacterComponents.Add(entityEnemy);

		var spawnPoint = sceneData.EnemySpawnPoints[0];

		var enemyInstance = Object.Instantiate(staticData.Enemies.PrefabsTurret[0], spawnPoint);
	}
}
