using Leopotam.EcsLite;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInitSystem : IEcsInitSystem
{
	public void Init(IEcsSystems systems)
	{
		EcsWorld world = systems.GetWorld();
		SharedData sharedData = systems.GetShared<SharedData>();

		var sceneData = sharedData.SceneData;

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
			
			EcsPool<AttackComponent> poolAttackComponent = world.GetPool<AttackComponent>();
			EcsPool<StateAttackComponent> poolStateAttackComponents = world.GetPool<StateAttackComponent>();
			EcsPool<DashComponent> poolDashComponent = world.GetPool<DashComponent>();
			EcsPool<HealthComponent> poolHeathComponents = world.GetPool<HealthComponent>();
			EcsPool<TargetComponent> poolTargetComponents = world.GetPool<TargetComponent>();
			EcsPool<EnablerComponent> poolEnablerComponents = world.GetPool<EnablerComponent>();
			EcsPool<CloseCombatComponent> poolCloseCombats = world.GetPool<CloseCombatComponent>();
			
			EcsPool<DamageComponent> poolDamage = world.GetPool<DamageComponent>();
			EcsPool<AIEnemyComponent> poolAIEnemyComponents = world.GetPool<AIEnemyComponent>();

			EcsPool<HitRangeComponent> poolRangeHit = world.GetPool<HitRangeComponent>();
			EcsPool<HitMeComponent> poolHitComponents = world.GetPool<HitMeComponent>();
			EcsPool<HitListComponent> poolHitList = world.GetPool<HitListComponent>();
			EcsPool<WeaponTypeComponent> poolWeaponTypes = world.GetPool<WeaponTypeComponent>();

			bool hasArsenal = false;

			if (enemies[i].TryGetComponent<Arsenal>(out Arsenal enemyArsenal))
			{
				EcsPool<WeaponComponent> poolWeapons = world.GetPool<WeaponComponent>();
				EcsPool<ArsenalComponent> poolArsenals = world.GetPool<ArsenalComponent>();
				EcsPool<WeaponSpawnPointComponent> poolWeaponSpawnPoint = world.GetPool<WeaponSpawnPointComponent>();

				ref var arsenal = ref poolArsenals.Add(entityEnemy);
				ref var weapon = ref poolWeapons.Add(entityEnemy);
				ref var weaponSpawnPoint = ref poolWeaponSpawnPoint.Add(entityEnemy);
				arsenal.arsenal = enemyArsenal;
				arsenal.currentNumberWeapon = -1;
				arsenal.arsenal.Init(weaponSpawnPoint.weaponSpawPoint);
				hasArsenal = true;
			}

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
			ref var attackComponent = ref poolAttackComponent.Add(entityEnemy);
			ref var dashComponent = ref poolDashComponent.Add(entityEnemy);
			ref var rangeHit = ref poolRangeHit.Add(entityEnemy);
			ref var damage = ref poolDamage.Add(entityEnemy);
			ref var hitList = ref poolHitList.Add(entityEnemy);
			ref var weaponType = ref poolWeaponTypes.Add(entityEnemy);

			enablerComponent.instance = enemies[i];

			enemyComponent.enemySettings = enemies[i].GetComponent<EnemySettings>();

			var animEvents = enemies[i].GetComponent<AnimationEvents>();
			animEvents.Init();
			characterComponent.characterController = enemies[i].GetComponent<CharacterController>();
			var characterSettings = enemies[i].GetComponent<CharacterSettings>();
			healthComponent.damageable = enemies[i].GetComponent<Damageable>();

			closeCombat.closeCombat = new CloseCombat(animEvents);
			aiEnemyComponent.enemyAgent = new AIEnemyAgent();
			aiEnemyComponent.enemyAgent.SetTransform(enemies[i].transform);

			animatorComponent.animationsManager = new EnemyMeleeAnimationsManager(enemies[i].GetComponent<Animator>(), animEvents);

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

			hitList.hitList = new List<int>(4);

			aiEnemyComponent.enemyAgent.SetDistanceRangeAttack(enemyComponent.enemySettings.DistanceRangeAttack);
			aiEnemyComponent.enemyAgent.SetRangeDetection(enemyComponent.enemySettings.RangeDetectTarget);
			aiEnemyComponent.enemyAgent.SetRangeMeleeAttack(enemyComponent.enemySettings.RangeMeleeAttack);
			aiEnemyComponent.enemyAgent.SetTypeAttack(enemyComponent.enemySettings.TypeEnemy == TypeEnemy.Melee ?
				TypeAttack.Melee : TypeAttack.Range);

			if (hasArsenal == false)
			{
				weaponType.typeWeapon = enemyComponent.enemySettings.TypeEnemy == TypeEnemy.Melee ?
				TypeWeapon.MELEE : TypeWeapon.GUN;

				damage.damage = enemyComponent.enemySettings.MeleeDamage;
			}
		}
	}
}
