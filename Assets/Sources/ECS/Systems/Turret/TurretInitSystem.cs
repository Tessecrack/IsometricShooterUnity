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
			EcsPool<RotatableComponent> poolRotatableComponents = world.GetPool<RotatableComponent>();
			EcsPool<AIEnemyComponent> poolAIEnemyComponents = world.GetPool<AIEnemyComponent>();
			EcsPool<TargetComponent> poolTargetComponents = world.GetPool<TargetComponent>();
			EcsPool<StateAttackComponent> poolStateAttackComponents = world.GetPool<StateAttackComponent>();
			EcsPool<CharacterComponent> poolCharacterComponents = world.GetPool<CharacterComponent>();
			EcsPool<WeaponComponent> poolWeaponComponents = world.GetPool<WeaponComponent>();
			EcsPool<AttackComponent> poolAttackComponents = world.GetPool<AttackComponent>();
			EcsPool<CharacterEventsComponent> poolEventsComponents = world.GetPool<CharacterEventsComponent>();

			ref var turretComponent = ref poolTurretComponents.Add(entityTurret);
			ref var rotatableComponent = ref poolRotatableComponents.Add(entityTurret);
			ref var aiEnemyComponent = ref poolAIEnemyComponents.Add(entityTurret);
			ref var targetComponent = ref poolTargetComponents.Add(entityTurret);
			ref var stateAttack = ref poolStateAttackComponents.Add(entityTurret);
			ref var characterComponent = ref poolCharacterComponents.Add(entityTurret);
			ref var weaponComponent = ref poolWeaponComponents.Add(entityTurret);
			ref var attackComponent = ref poolAttackComponents.Add(entityTurret);
			ref var eventComponent = ref poolEventsComponents.Add(entityTurret);

			var randomTurretNumber = new System.Random().Next(0, enemyTurretPrefabs.Count);
			var enemyTurretPrefab = enemyTurretPrefabs[randomTurretNumber];

			var turretInstance = UnityEngine.Object.Instantiate(enemyTurretPrefab, 
				turretsSpawnPoints[i].position, 
				Quaternion.identity);
			rotatableComponent.coefSmooth = 0.05f;
			turretComponent.turretSettings = turretInstance.GetComponent<TurretSettings>();
			aiEnemyComponent.enemyAgent = turretInstance.GetComponent<AIEnemyAgent>();

			characterComponent.characterController = turretComponent.turretSettings.GetCharacterController();
			characterComponent.characterTransform = turretComponent.turretSettings.GetTransform();
			weaponComponent.currentNumberWeapon = 0;
			weaponComponent.pointSpawnWeapon = null;
			weaponComponent.weapon = turretComponent.turretSettings.GetWeapon();

			attackComponent.attackerTransform = turretInstance.transform;
		}
	}
}
