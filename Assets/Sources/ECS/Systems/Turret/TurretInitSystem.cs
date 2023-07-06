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
			EcsPool<AIEnemyComponent> poolAIEnemyComponents = world.GetPool<AIEnemyComponent>();
			EcsPool<TargetComponent> poolTargetComponents = world.GetPool<TargetComponent>();
			EcsPool<StateAttackComponent> poolStateAttackComponents = world.GetPool<StateAttackComponent>();
			EcsPool<CharacterComponent> poolCharacterComponents = world.GetPool<CharacterComponent>();
			EcsPool<WeaponComponent> poolWeaponComponents = world.GetPool<WeaponComponent>();
			EcsPool<AttackComponent> poolAttackComponents = world.GetPool<AttackComponent>();
			EcsPool<CharacterEventsComponent> poolEventsComponents = world.GetPool<CharacterEventsComponent>();
			EcsPool<HealthComponent> poolHeathComponents = world.GetPool<HealthComponent>();
			EcsPool<EnablerComponent> poolEnablerComponents = world.GetPool<EnablerComponent>();
			EcsPool<HitMeComponent> poolHitComponents = world.GetPool<HitMeComponent>();
			EcsPool<EnemyComponent> poolEnemyComponents = world.GetPool<EnemyComponent>();

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

			var turretInstance = turretsInstances[i];

			rotatableComponent.coefSmooth = 0.05f;

			turretComponent.turretSettings = turretInstance.GetComponent<TurretSettings>();
			aiEnemyComponent.enemyAgent = turretInstance.GetComponent<AIEnemyAgent>();

			healthComponent.damageable = turretComponent.turretSettings.GetDamageable();
			healthComponent.maxHealth = turretComponent.turretSettings.GetMaxHealth();
			healthComponent.currentHealth = healthComponent.maxHealth;
			enablerComponent.instance = turretInstance;

			characterComponent.characterController = turretComponent.turretSettings.GetCharacterController();
			characterComponent.characterTransform = turretComponent.turretSettings.GetTransform();

			weaponComponent.pointSpawnWeapon = null;
			weaponComponent.weapon = turretComponent.turretSettings.GetWeapon();

			attackComponent.attackerTransform = turretInstance.transform;
			attackComponent.typeAttack = TypeAttack.Range;
		}
	}
}
