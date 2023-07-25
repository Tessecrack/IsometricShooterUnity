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
			EcsPool<MeleeAttackComponent> poolMeleeAttacks = world.GetPool<MeleeAttackComponent>();
			
			EcsPool<DamageComponent> poolDamage = world.GetPool<DamageComponent>();
			EcsPool<AIComponent> poolAIEnemyComponents = world.GetPool<AIComponent>();

			EcsPool<HitRangeComponent> poolRangeHit = world.GetPool<HitRangeComponent>();
			EcsPool<HitMeComponent> poolHitComponents = world.GetPool<HitMeComponent>();
			EcsPool<HitListComponent> poolHitList = world.GetPool<HitListComponent>();
			EcsPool<WeaponTypeComponent> poolWeaponTypes = world.GetPool<WeaponTypeComponent>();
			EcsPool<VelocityComponent> poolVelocities = world.GetPool<VelocityComponent>();

			ref var velocity = ref poolVelocities.Add(entityEnemy);
			ref var enemyComponent = ref poolEnemyComponents.Add(entityEnemy);
			ref var enablerComponent = ref poolEnablerComponents.Add(entityEnemy);

			enablerComponent.instance = enemies[i];
			enemyComponent.enemySettings = enemies[i].GetComponent<EnemySettings>();

			if (enemyComponent.enemySettings.HasArsenal)
			{
				EcsPool<WeaponComponent> poolWeapons = world.GetPool<WeaponComponent>();
				EcsPool<ArsenalComponent> poolArsenals = world.GetPool<ArsenalComponent>();
				EcsPool<WeaponSpawnPointComponent> poolWeaponSpawnPoint = world.GetPool<WeaponSpawnPointComponent>();
				var enemyArsenal = enemies[i].GetComponent<Arsenal>();
				ref var arsenal = ref poolArsenals.Add(entityEnemy);
				ref var weapon = ref poolWeapons.Add(entityEnemy);
				ref var weaponSpawnPoint = ref poolWeaponSpawnPoint.Add(entityEnemy);
				arsenal.arsenal = enemyArsenal;
				arsenal.currentNumberWeapon = -1;
				arsenal.arsenal.Init(weaponSpawnPoint.weaponSpawPoint);
			}

			ref var movableComponent = ref poolMovableComponents.Add(entityEnemy);
			ref var rotatableComponent = ref poolRotatableComponents.Add(entityEnemy);
			ref var aiEnemyComponent = ref poolAIEnemyComponents.Add(entityEnemy);
			ref var healthComponent = ref poolHeathComponents.Add(entityEnemy);
			ref var targetComponent = ref poolTargetComponents.Add(entityEnemy);
			ref var stateAttackComponent = ref poolStateAttackComponents.Add(entityEnemy);
			ref var characterComponent = ref poolCharacterComponents.Add(entityEnemy);
			ref var eventComponent = ref poolEventsComponents.Add(entityEnemy);
			ref var animatorComponent = ref poolAnimatorComponents.Add(entityEnemy);
			ref var hitComponent = ref poolHitComponents.Add(entityEnemy);
			ref var meleeAttack = ref poolMeleeAttacks.Add(entityEnemy);
			ref var attackComponent = ref poolAttackComponent.Add(entityEnemy);
			ref var dashComponent = ref poolDashComponent.Add(entityEnemy);
			ref var rangeHit = ref poolRangeHit.Add(entityEnemy);
			ref var damage = ref poolDamage.Add(entityEnemy);
			ref var hitList = ref poolHitList.Add(entityEnemy);
			ref var weaponType = ref poolWeaponTypes.Add(entityEnemy);

			var animEvents = enemies[i].GetComponent<AnimationEvents>();
			animEvents.Init();
			characterComponent.characterController = enemies[i].GetComponent<CharacterController>();
			var characterSettings = enemies[i].GetComponent<CharacterSettings>();
			healthComponent.damageable = enemies[i].GetComponent<Damageable>();

			meleeAttack.meleeAttack = new MeleeAttack(animEvents);
			aiEnemyComponent.aiAgent = new AIEnemyAgent();
			aiEnemyComponent.aiAgent.SetTransform(enemies[i].transform);

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
			rangeHit.rangeHit = aiEnemyComponent.aiAgent.DistanceAttack;

			hitList.hitList = new List<int>(4);

			aiEnemyComponent.aiAgent.SetDistanceRangeAttack(enemyComponent.enemySettings.DistanceRangeAttack);
			aiEnemyComponent.aiAgent.SetDistanceMeleeAttack(enemyComponent.enemySettings.DistanceMeleeAttack);
			aiEnemyComponent.aiAgent.SetRangeDetection(enemyComponent.enemySettings.RangeDetectTarget);
			
			aiEnemyComponent.aiAgent.SetHasArsenal(enemyComponent.enemySettings.HasArsenal);
			aiEnemyComponent.aiAgent.SetMeleeAttack(enemyComponent.enemySettings.HasMeleeAttack);
			aiEnemyComponent.aiAgent.SetRangeAttack(enemyComponent.enemySettings.HasRangeAttack);

			if (enemyComponent.enemySettings.HasArsenal == false)
			{
				if (enemyComponent.enemySettings.HasRangeAttack)
				{
					EcsPool<RangeAttackComponent> poolRangeAttack = world.GetPool<RangeAttackComponent>();
					ref var rangeAttack = ref poolRangeAttack.Add(entityEnemy);

					var rangeSettings = enemies[i].GetComponent<EnemyRangeSettings>();

					rangeAttack.rangeAttack = new RangeAttack(animEvents, 
						rangeSettings.PointSpawnProjectile, rangeSettings.ProjectilePrefab);

					rangeAttack.rangeAttack.SetOwner(enemies[i].transform);
					rangeAttack.rangeAttack.SetDamage(enemyComponent.enemySettings.RangeDamage);
					rangeAttack.rangeAttack.SetSpeedProjectile(rangeSettings.SpeedProjectile);

					weaponType.typeWeapon = TypeWeapon.HEAVY;
					damage.damage = enemyComponent.enemySettings.RangeDamage;
				}
				else
				{
					weaponType.typeWeapon = TypeWeapon.MELEE;
					damage.damage = enemyComponent.enemySettings.MeleeDamage;
				}
			}
		}
	}
}
