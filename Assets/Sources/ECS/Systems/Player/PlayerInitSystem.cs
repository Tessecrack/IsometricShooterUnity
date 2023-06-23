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

		EcsPool<PlayerComponent> poolPlayer = world.GetPool<PlayerComponent>();
		EcsPool<CharacterComponent> poolCharacterComponent = world.GetPool<CharacterComponent>();
		EcsPool<CharacterEventsComponent> poolCharacterEventComponent = world.GetPool<CharacterEventsComponent>();
		EcsPool<InputEventComponent> poolInputEventComponent = world.GetPool<InputEventComponent>();
		EcsPool<MovableComponent> poolMovableComponent = world.GetPool<MovableComponent>();
		EcsPool<RotatableComponent> poolRotatableComponent = world.GetPool<RotatableComponent>();
		EcsPool<AnimatorComponent> poolAnimatorComponent = world.GetPool<AnimatorComponent>();
		EcsPool<WeaponComponent> poolWeaponComponent = world.GetPool<WeaponComponent>();
		EcsPool<AttackComponent> poolAttackComponent = world.GetPool<AttackComponent>();
		EcsPool<StateAttackComponent> poolCharacterStateComponent = world.GetPool<StateAttackComponent>();
		EcsPool<DashComponent> poolDashComponent = world.GetPool<DashComponent>();
		EcsPool<HealthComponent> poolHealthComponent = world.GetPool<HealthComponent>();
		EcsPool<CharacterRigComponent> poolCharacterRigComponent = world.GetPool<CharacterRigComponent>();
		EcsPool<TargetComponent> poolTargetComponent = world.GetPool<TargetComponent>();
		EcsPool<EnablerComponent> poolEnablerComponent = world.GetPool<EnablerComponent>();
		EcsPool<CloseCombatComponent> poolCloseCombat = world.GetPool<CloseCombatComponent>();

		ref var playerComponent = ref poolPlayer.Add(entityPlayer);
		ref var characterComponent = ref poolCharacterComponent.Add(entityPlayer);
		ref var characterEventsComponent = ref poolCharacterEventComponent.Add(entityPlayer);
		ref var inputComponent = ref poolInputEventComponent.Add(entityPlayer);
		ref var movableComponent = ref poolMovableComponent.Add(entityPlayer);
		ref var rotatableComponent = ref poolRotatableComponent.Add(entityPlayer);
		ref var animatorComponent = ref poolAnimatorComponent.Add(entityPlayer);
		ref var weaponComponent = ref poolWeaponComponent.Add(entityPlayer);
		ref var attackComponent = ref poolAttackComponent.Add(entityPlayer);
		ref var characterState = ref poolCharacterStateComponent.Add(entityPlayer);
		ref var dashComponent = ref poolDashComponent.Add(entityPlayer);	
		ref var healthComponent = ref poolHealthComponent.Add(entityPlayer);
		ref var characterRigComponent = ref poolCharacterRigComponent.Add(entityPlayer);
		ref var targetComponent = ref poolTargetComponent.Add(entityPlayer);
		ref var enablerComponent = ref poolEnablerComponent.Add(entityPlayer);
		ref var closeCombatComponent = ref poolCloseCombat.Add(entityPlayer);

		characterState.stateAttackTime = 3;
		dashComponent.dashTime = 0.06f;
		dashComponent.dashSpeed = 80.0f;

		GameObject player = sceneData.PlayerInstance;

		inputComponent.userInput = player.GetComponent<UserInput>(); // TODO: NEED IMPROVE

		runtimeData.OwnerCameraTransform = player.transform.position;
		characterComponent.characterTransform = player.transform;

		var characterSettings = player.GetComponent<CharacterSettings>();
		characterComponent.characterSettings = characterSettings;

		player.transform.forward = staticData.GlobalForwardVector;
		characterComponent.instance = player;
		healthComponent.damageable = player.GetComponent<Damageable>();
		healthComponent.maxHealth = characterSettings.GetMaxHealth();
		healthComponent.currentHealth = healthComponent.maxHealth;

		closeCombatComponent.closeCombat = player.GetComponent<CloseCombat>();
		closeCombatComponent.closeCombat.SetTotalNumbersStrikes(characterSettings.TotalNumberStrikes);

		animatorComponent.animationsManager = new PlayerAnimationsManager(player.GetComponent<Animator>(),
			closeCombatComponent.closeCombat);

		animatorComponent.animationState = new CharacterAnimationState();

		characterComponent.characterController = player.GetComponent<CharacterController>();

		movableComponent.transform = player.transform;

		characterRigComponent.characterRigController = player.GetComponent<CharacterRigController>();

		movableComponent.moveSpeed = characterSettings.GetCharacterSpeed();

		targetComponent.target = runtimeData.CursorPosition;

		weaponComponent.pointSpawnWeapon = characterSettings.GetPointSpawnWeapon();

		weaponComponent.currentNumberWeapon = -1;

		movableComponent.coefSmooth = 0.3f;
		rotatableComponent.coefSmooth = 0.3f;

		enablerComponent.instance = player;
		enablerComponent.isEnabled = true;

		/*NEED IMPROVE; SPOILER: OBJECT POOL*/
		var weaponsPool = new WeaponsPool();
		weaponsPool.InitWeapons(staticData.Weapons.WeaponsPrefabs, weaponComponent.pointSpawnWeapon);

		weaponComponent.weaponsPool = weaponsPool; // TODO: NEED IMPROVE
	}
}
