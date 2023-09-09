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
			EcsPool<RotatableComponent> poolRotatableComponents = world.GetPool<RotatableComponent>();

			EcsPool<InputAttackComponent> poolAttackComponent = world.GetPool<InputAttackComponent>();
			EcsPool<AimTimerComponent> poolStateAttackComponents = world.GetPool<AimTimerComponent>();
			
			EcsPool<HealthComponent> poolHeathComponents = world.GetPool<HealthComponent>();
			EcsPool<TargetComponent> poolTargetComponents = world.GetPool<TargetComponent>();
			EcsPool<EnablerComponent> poolEnablerComponents = world.GetPool<EnablerComponent>();

			EcsPool<DamageComponent> poolDamage = world.GetPool<DamageComponent>();
			EcsPool<AIComponent> poolAIEnemyComponents = world.GetPool<AIComponent>();

			EcsPool<HitRangeComponent> poolRangeHit = world.GetPool<HitRangeComponent>();
			EcsPool<HitMeComponent> poolHitComponents = world.GetPool<HitMeComponent>();
			EcsPool<HitListComponent> poolHitList = world.GetPool<HitListComponent>();
			EcsPool<WeaponTypeComponent> poolWeaponTypes = world.GetPool<WeaponTypeComponent>();
			EcsPool<VelocityComponent> poolVelocities = world.GetPool<VelocityComponent>();
			EcsPool<BaseAttackComponent> poolBaseAttacks = world.GetPool<BaseAttackComponent>();
			EcsPool<CharacterStateComponent> poolCharacterStates = world.GetPool<CharacterStateComponent>();
			EcsPool<AimStateComponent> poolAimStates = world.GetPool<AimStateComponent>();

			var characterSettings = enemies[i].GetComponent<CharacterSettings>();

			ref var characterState = ref poolCharacterStates.Add(entityEnemy);
			characterState.characterState = CharacterState.IDLE;

			ref var aimState = ref poolAimStates.Add(entityEnemy);
			aimState.aimState = AimState.NO_AIM;

			ref var velocity = ref poolVelocities.Add(entityEnemy);
			ref var enemyComponent = ref poolEnemyComponents.Add(entityEnemy);
			ref var enablerComponent = ref poolEnablerComponents.Add(entityEnemy);

			enablerComponent.instance = enemies[i];
			enemyComponent.enemySettings = enemies[i].GetComponent<EnemySettings>();

			ref var rotatableComponent = ref poolRotatableComponents.Add(entityEnemy);
			ref var aiEnemyComponent = ref poolAIEnemyComponents.Add(entityEnemy);
			ref var healthComponent = ref poolHeathComponents.Add(entityEnemy);
			ref var targetComponent = ref poolTargetComponents.Add(entityEnemy);
			ref var stateAttackComponent = ref poolStateAttackComponents.Add(entityEnemy);
			ref var characterComponent = ref poolCharacterComponents.Add(entityEnemy);
			ref var eventComponent = ref poolEventsComponents.Add(entityEnemy);
			ref var hitComponent = ref poolHitComponents.Add(entityEnemy);
			ref var attackComponent = ref poolAttackComponent.Add(entityEnemy);
			ref var rangeHit = ref poolRangeHit.Add(entityEnemy);
			ref var damage = ref poolDamage.Add(entityEnemy);
			ref var hitList = ref poolHitList.Add(entityEnemy);
			ref var weaponType = ref poolWeaponTypes.Add(entityEnemy);
			ref var baseAttack = ref poolBaseAttacks.Add(entityEnemy);

			if (characterSettings.IsMovable)
			{
				EcsPool<MovableComponent> poolMovableComponents = world.GetPool<MovableComponent>();
				EcsPool<DashComponent> poolDashComponent = world.GetPool<DashComponent>();

				ref var movableComponent = ref poolMovableComponents.Add(entityEnemy);
				ref var dashComponent = ref poolDashComponent.Add(entityEnemy);

				movableComponent.coefSmooth = 0.3f;
				movableComponent.transform = enemies[i].transform;
				movableComponent.moveSpeed = characterSettings.RunSpeed;
			}
			characterComponent.characterController = enemies[i].GetComponent<CharacterController>();
			
			healthComponent.damageable = enemies[i].GetComponent<Damageable>();

			aiEnemyComponent.aiAgent = new AIEnemyAgent();
			aiEnemyComponent.aiAgent.SetTransform(enemies[i].transform);	

			characterComponent.characterTransform = enemies[i].transform;
			characterComponent.characterSettings = characterSettings;
			healthComponent.maxHealth = characterSettings.MaxHealth;
			healthComponent.currentHealth = healthComponent.maxHealth;

			rotatableComponent.coefSmooth = 0.3f;
			rangeHit.rangeHit = aiEnemyComponent.aiAgent.DistanceAttack;

			hitList.hitList = new List<int>(4);

			aiEnemyComponent.aiAgent.SetDistanceRangeAttack(enemyComponent.enemySettings.DistanceRangeAttack);
			aiEnemyComponent.aiAgent.SetDistanceMeleeAttack(enemyComponent.enemySettings.DistanceMeleeAttack);
			aiEnemyComponent.aiAgent.SetRangeDetection(enemyComponent.enemySettings.RangeDetectTarget);

			aiEnemyComponent.aiAgent.SetMeleeAttack(enemyComponent.enemySettings.HasMeleeAttack);
			aiEnemyComponent.aiAgent.SetRangeAttack(enemyComponent.enemySettings.HasRangeAttack);

			var animEvents = enemies[i].GetComponent<AnimationEvents>();

			if (enemyComponent.enemySettings.HasRangeAttack)
			{
				var rangeSettings = enemies[i].GetComponent<EnemyRangeSettings>();

				var shooter = new SingleShooter();
				shooter.SetProjectile(rangeSettings.ProjectilePrefab);
				shooter.SetQuantityOneShotProjectile(1);
				shooter.SetSpawnPointsShot(rangeSettings.PointsSpawnProjectile);
				shooter.SetSpeedProjectile(rangeSettings.SpeedProjectile);
				shooter.SetDamage(enemyComponent.enemySettings.RangeDamage);

				if (animEvents != null)
				{
					animEvents.Init(characterSettings.CountAnimationsRangeAttack);
					baseAttack.baseAttack = new RangeAttackEvent(animEvents, shooter);
				}
				else
				{
					var rangeAutomaticAttack = new RangeAutomaticAttack(shooter);
					rangeAutomaticAttack.SetHasTarget(false);
					rangeAutomaticAttack.SetDelayBetweenAttack(rangeSettings.DelayBetweenAttack);
					baseAttack.baseAttack = rangeAutomaticAttack;
				}
				damage.damage = enemyComponent.enemySettings.RangeDamage;
			}
			else
			{
				if (animEvents != null)
				{
					animEvents.Init(characterSettings.CountAnimationsMeleeAttack);
					var meleeAttack = new MeleeAttackEvent(animEvents);
					damage.damage = enemyComponent.enemySettings.MeleeDamage;
					baseAttack.baseAttack = meleeAttack;
				}
			}

			if (enemies[i].TryGetComponent<Animator>(out var animator))
			{
				EcsPool<AnimatorComponent> poolAnimatorComponents = world.GetPool<AnimatorComponent>();
				ref var animatorComponent = ref poolAnimatorComponents.Add(entityEnemy);
				animatorComponent.animationState = new CharacterAnimationState();
				animatorComponent.animationsManager = new EnemyAnimationsManager(animator, animEvents,
					baseAttack.baseAttack.TypeAttack);
				weaponType.typeWeapon = TypeWeapon.NO_WEAPON;
			}
		}
	}
}
