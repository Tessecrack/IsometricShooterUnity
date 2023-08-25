using Leopotam.EcsLite;
using System.Collections.Generic;
using UnityEngine;

public class TurretInitSystem : IEcsInitSystem
{
	public void Init(IEcsSystems systems)
	{
		EcsWorld world = systems.GetWorld();
		SharedData sharedData = systems.GetShared<SharedData>();

		var sceneData = sharedData.SceneData;
		var turrets = sceneData.TurretsInstances;
		SpawnTurrets(world, turrets);
	}

	private void SpawnTurrets(EcsWorld world, List<GameObject> turretsInstances)
	{
		for (int i = 0; i < turretsInstances.Count; ++i)
		{
			int entityTurret = world.NewEntity();

			EcsPool<TurretComponent> poolTurretComponents = world.GetPool<TurretComponent>();
			EcsPool<RotatableComponent> poolRotatableComponents = world.GetPool<RotatableComponent>();
			EcsPool<AIComponent> poolAIEnemyComponents = world.GetPool<AIComponent>();
			EcsPool<TargetComponent> poolTargetComponents = world.GetPool<TargetComponent>();
			EcsPool<AimTimerComponent> poolStateAttackComponents = world.GetPool<AimTimerComponent>();
			EcsPool<CharacterComponent> poolCharacterComponents = world.GetPool<CharacterComponent>();
			EcsPool<WeaponComponent> poolWeaponComponents = world.GetPool<WeaponComponent>();
			EcsPool<InputAttackComponent> poolAttackComponents = world.GetPool<InputAttackComponent>();
			EcsPool<CharacterEventsComponent> poolEventsComponents = world.GetPool<CharacterEventsComponent>();
			EcsPool<HealthComponent> poolHeathComponents = world.GetPool<HealthComponent>();
			EcsPool<EnablerComponent> poolEnablerComponents = world.GetPool<EnablerComponent>();
			EcsPool<HitMeComponent> poolHitComponents = world.GetPool<HitMeComponent>();
			EcsPool<EnemyComponent> poolEnemyComponents = world.GetPool<EnemyComponent>();
			EcsPool<WeaponTypeComponent> poolWeaponTypeComponents = world.GetPool<WeaponTypeComponent>();
			EcsPool<BaseAttackComponent> poolBaseAttacks = world.GetPool<BaseAttackComponent>();

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
			ref var enablerComponent = ref poolEnablerComponents.Add(entityTurret);
			ref var hitComponent = ref poolHitComponents.Add(entityTurret);
			ref var enemyComponent = ref poolEnemyComponents.Add(entityTurret);
			ref var weaponTypeComponent = ref poolWeaponTypeComponents.Add(entityTurret);
			ref var baseAttack = ref poolBaseAttacks.Add(entityTurret);

			var turretInstance = turretsInstances[i];

			rotatableComponent.coefSmooth = 0.05f;

			turretComponent.turretSettings = turretInstance.GetComponent<TurretSettings>();

			turretComponent.turretSettings.Init();

			aiEnemyComponent.aiAgent = new AIEnemyAgent();
			aiEnemyComponent.aiAgent.SetTransform(turretInstance.transform);

			healthComponent.damageable = turretComponent.turretSettings.GetDamageable();
			healthComponent.maxHealth = turretComponent.turretSettings.GetMaxHealth();
			healthComponent.currentHealth = healthComponent.maxHealth;
			enablerComponent.instance = turretInstance;

			characterComponent.characterController = turretComponent.turretSettings.GetCharacterController();
			characterComponent.characterTransform = turretComponent.turretSettings.GetTransform();

			weaponComponent.weapon = turretComponent.turretSettings.GetWeapon();
			weaponComponent.weapon.Init();
			baseAttack.baseAttack = weaponComponent.weapon.BaseAttack;

			attackComponent.attackerTransform = turretInstance.transform;
			attackComponent.typeAttack = TypeAttack.RANGE;

			weaponTypeComponent.typeWeapon = TypeWeapon.HEAVY;

			aiEnemyComponent.aiAgent.SetRangeAttack(true);
			aiEnemyComponent.aiAgent.SetRangeDetection(turretComponent.turretSettings.RangeDetection);
			aiEnemyComponent.aiAgent.SetDistanceRangeAttack(turretComponent.turretSettings.RangeDetection);
		}
	}
}
