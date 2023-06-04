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

		var redTurretsSpawnPoints = sceneData.RedTurretsSpawnPoints;
		var blueTurretsSpawnPoints = sceneData.BlueTurretsSpawnPoints;

		var redTurretPrefab = staticData.EnemyTurrets.PrefabRedTurrets;
		var blueTurretPrefab = staticData.EnemyTurrets.PrefabBlueTurrets;

		SpawnTurrets(world, redTurretPrefab, redTurretsSpawnPoints);
		SpawnTurrets(world, blueTurretPrefab, blueTurretsSpawnPoints);
	}


	private void SpawnTurrets(EcsWorld world, GameObject turretPrefab, Transform[] turretsSpawnPoints, bool isFriend = false)
	{
		for (int i = 1; i < turretsSpawnPoints.Length; ++i)
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
			EcsPool<HealthComponent> poolHeathComponents = world.GetPool<HealthComponent>();

			ref var turretComponent = ref poolTurretComponents.Add(entityTurret);
			ref var rotatableComponent = ref poolRotatableComponents.Add(entityTurret);
			ref var aiEnemyComponent = ref poolAIEnemyComponents.Add(entityTurret);
			ref var targetComponent = ref poolTargetComponents.Add(entityTurret);
			ref var stateAttack = ref poolStateAttackComponents.Add(entityTurret);
			ref var characterComponent = ref poolCharacterComponents.Add(entityTurret);
			ref var weaponComponent = ref poolWeaponComponents.Add(entityTurret);
			ref var attackComponent = ref poolAttackComponents.Add(entityTurret);
			ref var eventComponent = ref poolEventsComponents.Add(entityTurret);
			ref var healthComponent = ref poolHeathComponents.Add(entityTurret);

			var turretInstance = UnityEngine.Object.Instantiate(turretPrefab,
				turretsSpawnPoints[i].position,
				Quaternion.identity);

			rotatableComponent.coefSmooth = 0.05f;

			turretComponent.turretSettings = turretInstance.GetComponent<TurretSettings>();
			aiEnemyComponent.enemyAgent = turretInstance.GetComponent<AIEnemyAgent>();

			healthComponent.damageable = turretComponent.turretSettings.GetDamageable();
			healthComponent.maxHealth = turretComponent.turretSettings.GetMaxHealth();
			healthComponent.currentHealth = healthComponent.maxHealth;

			characterComponent.instance = turretInstance;
			characterComponent.characterController = turretComponent.turretSettings.GetCharacterController();
			characterComponent.characterTransform = turretComponent.turretSettings.GetTransform();

			weaponComponent.currentNumberWeapon = 0;
			weaponComponent.pointSpawnWeapon = null;
			weaponComponent.weapon = turretComponent.turretSettings.GetWeapon();

			attackComponent.attackerTransform = turretInstance.transform;
		}
	}
}
