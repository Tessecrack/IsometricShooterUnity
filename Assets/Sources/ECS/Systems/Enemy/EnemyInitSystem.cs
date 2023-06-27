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
			EcsPool<MovableComponent> poolMovableComponents = world.GetPool<MovableComponent>();
			EcsPool<RotatableComponent> poolRotatableComponents = world.GetPool<RotatableComponent>();
			EcsPool<AIEnemyComponent> poolAIEnemyComponents = world.GetPool<AIEnemyComponent>();
			EcsPool<HealthComponent> poolHeathComponents = world.GetPool<HealthComponent>();
			EcsPool<EnablerComponent> poolEnablerComponents = world.GetPool<EnablerComponent>();
			EcsPool<HitComponent> poolHitComponents = world.GetPool<HitComponent>();
			EcsPool<TargetComponent> poolTargetComponents = world.GetPool<TargetComponent>();
			EcsPool<StateAttackComponent> poolStateAttackComponents = world.GetPool<StateAttackComponent>();
			EcsPool<CharacterComponent> poolCharacterComponents = world.GetPool<CharacterComponent>();

			EcsPool<CharacterEventsComponent> poolEventsComponents = world.GetPool<CharacterEventsComponent>();
			EcsPool<AnimatorComponent> poolAnimatorComponents = world.GetPool<AnimatorComponent>();

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

			characterComponent.instance = enemies[i];
			enablerComponent.instance = enemies[i];
			characterComponent.characterController = enemies[i].GetComponent<CharacterController>();
			characterComponent.characterTransform = enemies[i].transform;

			var characterSettings = enemies[i].GetComponent<CharacterSettings>();

			characterComponent.characterSettings = characterSettings;
			aiEnemyComponent.enemyAgent = enemies[i].GetComponent<AIEnemyAgent>();

			healthComponent.damageable = enemies[i].GetComponent<Damageable>();
			healthComponent.maxHealth = characterSettings.GetMaxHealth();
			healthComponent.currentHealth = healthComponent.maxHealth;

			animatorComponent.animationsManager = new EnemyMeleeAnimationsManager(enemies[i].GetComponent<Animator>());
			animatorComponent.animationState = new CharacterAnimationState();

			movableComponent.coefSmooth = 0.3f;
			rotatableComponent.coefSmooth = 0.3f;
		}
	}
}
