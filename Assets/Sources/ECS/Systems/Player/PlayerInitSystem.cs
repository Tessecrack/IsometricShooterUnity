using Leopotam.EcsLite;
using UnityEngine;

public class PlayerInitSystem : IEcsInitSystem
{
	public void Init(IEcsSystems systems)
	{
		EcsWorld world = systems.GetWorld();
		int entityPlayer = world.NewEntity();
		SharedData sharedData = systems.GetShared<SharedData>();

		var staticData = sharedData.StaticData;
		var sceneData = sharedData.SceneData;
		var runtimeData = sharedData.RuntimeData;

		EcsPool<CharacterComponent> poolCharacterComponent = world.GetPool<CharacterComponent>();
		EcsPool<CharacterEventsComponent> poolCharacterEventComponent = world.GetPool<CharacterEventsComponent>();
		EcsPool<InputEventComponent> poolInputEventComponent = world.GetPool<InputEventComponent>();
		EcsPool<MovableComponent> poolMovableComponent = world.GetPool<MovableComponent>();
		EcsPool<AnimatorComponent> poolAnimatorComponent = world.GetPool<AnimatorComponent>();
		EcsPool<WeaponComponent> poolWeaponComponent = world.GetPool<WeaponComponent>();
		EcsPool<AttackComponent> poolAttackComponent = world.GetPool<AttackComponent>();
		EcsPool<CharacterStateAttackComponent> poolCharacterStateComponent = world.GetPool<CharacterStateAttackComponent>();
		EcsPool<DashComponent> poolDashComponent = world.GetPool<DashComponent>();
		EcsPool<HealthComponent> poolHealthComponent = world.GetPool<HealthComponent>();

		ref var characterComponent = ref poolCharacterComponent.Add(entityPlayer);
		ref var characterEventsComponent = ref poolCharacterEventComponent.Add(entityPlayer);
		ref var inputComponent = ref poolInputEventComponent.Add(entityPlayer);
		ref var movableComponent = ref poolMovableComponent.Add(entityPlayer);
		ref var animatorComponent = ref poolAnimatorComponent.Add(entityPlayer);
		ref var weaponComponent = ref poolWeaponComponent.Add(entityPlayer);
		ref var attackComponent = ref poolAttackComponent.Add(entityPlayer);
		ref var characterState = ref poolCharacterStateComponent.Add(entityPlayer);
		ref var dashComponent = ref poolDashComponent.Add(entityPlayer);	
		ref var healthComponent = ref poolHealthComponent.Add(entityPlayer);

		characterState.stateAttackTime = 3;
		dashComponent.dashTime = 0.06f;
		dashComponent.dashSpeed = 80.0f;

		GameObject player = Object.Instantiate(staticData.PlayerPrefab, sceneData.playerSpawnPoint.position, Quaternion.identity);
		runtimeData.OwnerCameraTransform = player.transform;
		characterComponent.currentPosition = player.transform;

		var characterSettings = player.GetComponent<CharacterSettings>();
		characterComponent.characterSettings = characterSettings;
		player.transform.forward = staticData.GlobalForwardVector;

		healthComponent.maxHealth = characterSettings.maxHealth;
		healthComponent.currentHealth = healthComponent.maxHealth;

		animatorComponent.animationsManager = player.GetComponent<CharacterAnimationsManager>();
		animatorComponent.animationState = new CharacterAnimationState();
		characterComponent.characterController = player.GetComponent<CharacterController>();

		movableComponent.transform = player.transform;

		characterComponent.characterRigController = player.GetComponent<CharacterRigController>();

		movableComponent.moveSpeed = characterSettings.CharacterSpeed;
		weaponComponent.pointSpawnWeapon = characterSettings.PointSpawnWeapon;

		weaponComponent.currentNumberWeapon = -1;


		/*NEED IMPROVE; SPOILER: OBJECT POOL*/
		var weaponsPool = new WeaponsPool();
		weaponsPool.InitWeapons(staticData.Weapons.WeaponsPrefabs, weaponComponent.pointSpawnWeapon);

		weaponComponent.weaponsPool = weaponsPool; // TODO: NEED IMPROVE
	}
}
