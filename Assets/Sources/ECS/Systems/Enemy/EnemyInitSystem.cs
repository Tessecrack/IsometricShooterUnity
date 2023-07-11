using Leopotam.EcsLite;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInitSystem : IEcsInitSystem
{
	public void Init(IEcsSystems systems)
	{
		EcsWorld world = systems.GetWorld();
		SharedData sharedData = systems.GetShared<SharedData>();

		var staticData = sharedData.StaticData;
		var sceneData = sharedData.SceneData;
		var runtimeData = sharedData.RuntimeData;

		var enemies = sceneData.EnemmiesInstances;

		SpawnEnemies(world, enemies);
	}

	private void SpawnEnemies(EcsWorld world, List<GameObject> enemies)
	{
		for (int i = 0; i < enemies.Count; ++i)
		{
			int entityEnemy = world.NewEntity();

			EcsPool<EnemyComponent> poolEnemyComponents = world.GetPool<EnemyComponent>();
			EcsPool<CharacterComponent> poolCharacterComponents = world.GetPool<CharacterComponent>();
			EcsPool<CharacterEventsComponent> poolEventsComponents = world.GetPool<CharacterEventsComponent>();
			EcsPool<MovableComponent> poolMovableComponents = world.GetPool<MovableComponent>();
			EcsPool<RotatableComponent> poolRotatableComponents = world.GetPool<RotatableComponent>();
			EcsPool<AnimatorComponent> poolAnimatorComponents = world.GetPool<AnimatorComponent>();
			EcsPool<WeaponComponent> poolWeapons = world.GetPool<WeaponComponent>();
			EcsPool<AttackComponent> poolAttackComponent = world.GetPool<AttackComponent>();
			EcsPool<StateAttackComponent> poolStateAttackComponents = world.GetPool<StateAttackComponent>();
			EcsPool<DashComponent> poolDashComponent = world.GetPool<DashComponent>();
			EcsPool<HealthComponent> poolHeathComponents = world.GetPool<HealthComponent>();
			EcsPool<TargetComponent> poolTargetComponents = world.GetPool<TargetComponent>();
			EcsPool<EnablerComponent> poolEnablerComponents = world.GetPool<EnablerComponent>();
			EcsPool<CloseCombatComponent> poolCloseCombats = world.GetPool<CloseCombatComponent>();
			EcsPool<ArsenalComponent> poolArsenals = world.GetPool<ArsenalComponent>();
			EcsPool<HitRangeComponent> poolRangeHit = world.GetPool<HitRangeComponent>();
			EcsPool<HitMeComponent> poolHitComponents = world.GetPool<HitMeComponent>();
			EcsPool<DamageComponent> poolDamage = world.GetPool<DamageComponent>();
			EcsPool<WeaponSpawnPointComponent> poolWeaponSpawnPoint = world.GetPool<WeaponSpawnPointComponent>();
			EcsPool<AIEnemyComponent> poolAIEnemyComponents = world.GetPool<AIEnemyComponent>();

			ref var enemyComponent = ref poolEnemyComponents.Add(entityEnemy);
			ref var movableComponent = ref poolMovableComponents.Add(entityEnemy);
			ref var rotatableComponent = ref poolRotatableComponents.Add(entityEnemy);
			ref var aiEnemyComponent = ref poolAIEnemyComponents.Add(entityEnemy);
			ref var healthComponent = ref poolHeathComponents.Add(entityEnemy);
			ref var enablerComponent = ref poolEnablerComponents.Add(entityEnemy);

			ref var targetComponent = ref poolTargetComponents.Add(entityEnemy);
			ref var stateAttackComponent = ref poolStateAttackComponents.Add(entityEnemy);
			ref var characterComponent = ref poolCharacterComponents.Add(entityEnemy);

			ref var eventComponent = ref poolEventsComponents.Add(entityEnemy);
			ref var animatorComponent = ref poolAnimatorComponents.Add(entityEnemy);
			ref var hitComponent = ref poolHitComponents.Add(entityEnemy);
			
			ref var closeCombat = ref poolCloseCombats.Add(entityEnemy);

			ref var arsenal = ref poolArsenals.Add(entityEnemy);
			ref var weapon = ref poolWeapons.Add(entityEnemy);
			ref var attackComponent = ref poolAttackComponent.Add(entityEnemy);
			ref var dashComponent = ref poolDashComponent.Add(entityEnemy);
			ref var rangeHit = ref poolRangeHit.Add(entityEnemy);

			ref var damage = ref poolDamage.Add(entityEnemy);
			ref var weaponSpawnPoint = ref poolWeaponSpawnPoint.Add(entityEnemy);

			enablerComponent.instance = enemies[i];
			characterComponent.characterController = enemies[i].GetComponent<CharacterController>();
			var characterSettings = enemies[i].GetComponent<CharacterSettings>();
			aiEnemyComponent.enemyAgent = enemies[i].GetComponent<AIEnemyAgent>();
			healthComponent.damageable = enemies[i].GetComponent<Damageable>();
			closeCombat.closeCombat = enemies[i].GetComponent<CloseCombat>();
			arsenal.arsenal = enemies[i].GetComponent<Arsenal>();
			arsenal.currentNumberWeapon = -1;

			arsenal.arsenal.InitArsenal(weaponSpawnPoint.weaponSpawPoint);

			animatorComponent.animationsManager = new EnemyMeleeAnimationsManager(enemies[i].GetComponent<Animator>(), closeCombat.closeCombat);

			characterComponent.characterTransform = enemies[i].transform;
			characterComponent.characterSettings = characterSettings;
			healthComponent.maxHealth = characterSettings.GetMaxHealth();
			healthComponent.currentHealth = healthComponent.maxHealth;

			animatorComponent.animationState = new CharacterAnimationState();

			movableComponent.coefSmooth = 0.3f;
			movableComponent.transform = enemies[i].transform;
			movableComponent.moveSpeed = characterSettings.GetCharacterSpeed();

			rotatableComponent.coefSmooth = 0.3f;
			rangeHit.rangeHit = aiEnemyComponent.enemyAgent.RangeAttack;
		}
	}
}
