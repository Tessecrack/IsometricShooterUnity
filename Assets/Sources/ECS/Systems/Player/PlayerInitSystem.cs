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
		EcsPool<InputAttackComponent> poolAttackComponent = world.GetPool<InputAttackComponent>();
		EcsPool<DashComponent> poolDashComponent = world.GetPool<DashComponent>();
		EcsPool<HealthComponent> poolHealthComponent = world.GetPool<HealthComponent>();
		EcsPool<CharacterRigComponent> poolCharacterRigComponent = world.GetPool<CharacterRigComponent>();
		EcsPool<TargetComponent> poolTargetComponent = world.GetPool<TargetComponent>();
		EcsPool<EnablerComponent> poolEnablerComponent = world.GetPool<EnablerComponent>();
		EcsPool<ArsenalComponent> poolArsenal = world.GetPool<ArsenalComponent>();
		EcsPool<HitRangeComponent> poolRangeHit = world.GetPool<HitRangeComponent>();
		EcsPool<DamageComponent> poolDamage = world.GetPool<DamageComponent>();
		EcsPool<WeaponSpawnPointComponent> poolWeaponSpawnPoint = world.GetPool<WeaponSpawnPointComponent>();
		EcsPool<HitMeComponent> poolHitMeComponents = world.GetPool<HitMeComponent>();
		EcsPool<WeaponTypeComponent> poolTypeWeapon = world.GetPool<WeaponTypeComponent>();
		EcsPool<VelocityComponent> poolVelocities = world.GetPool<VelocityComponent>();

		EcsPool<BaseAttackComponent> poolBaseAttacks = world.GetPool<BaseAttackComponent>();
		EcsPool<CharacterStateComponent> poolCharacterStates = world.GetPool<CharacterStateComponent>();
		EcsPool<AimStateComponent> poolAimStates = world.GetPool<AimStateComponent>();
		EcsPool<AimTimerComponent> poolAimTimerComponent = world.GetPool<AimTimerComponent>();
		ref var characterState = ref poolCharacterStates.Add(entityPlayer);
		characterState.characterState = CharacterState.IDLE;

		ref var aimState = ref poolAimStates.Add(entityPlayer);
		aimState.aimState = AimState.NO_AIM;

		ref var velocity = ref poolVelocities.Add(entityPlayer);
		ref var playerComponent = ref poolPlayer.Add(entityPlayer);
		ref var characterComponent = ref poolCharacterComponent.Add(entityPlayer);
		ref var characterEventsComponent = ref poolCharacterEventComponent.Add(entityPlayer);
		ref var inputComponent = ref poolInputEventComponent.Add(entityPlayer);
		ref var movableComponent = ref poolMovableComponent.Add(entityPlayer);
		ref var rotatableComponent = ref poolRotatableComponent.Add(entityPlayer);
		ref var animatorComponent = ref poolAnimatorComponent.Add(entityPlayer);
		ref var weaponComponent = ref poolWeaponComponent.Add(entityPlayer);
		ref var attackComponent = ref poolAttackComponent.Add(entityPlayer);
		ref var aimTimer = ref poolAimTimerComponent.Add(entityPlayer);
		ref var dashComponent = ref poolDashComponent.Add(entityPlayer);	
		ref var healthComponent = ref poolHealthComponent.Add(entityPlayer);
		ref var characterRigComponent = ref poolCharacterRigComponent.Add(entityPlayer);
		ref var targetComponent = ref poolTargetComponent.Add(entityPlayer);
		ref var enablerComponent = ref poolEnablerComponent.Add(entityPlayer);
		ref var arsenal = ref poolArsenal.Add(entityPlayer);
		ref var rangeHit = ref poolRangeHit.Add(entityPlayer);
		ref var damage = ref poolDamage.Add(entityPlayer);
		ref var weaponSpawnPoint = ref poolWeaponSpawnPoint.Add(entityPlayer);
		ref var hitMeComponent = ref poolHitMeComponents.Add(entityPlayer);
		ref var weaponType = ref poolTypeWeapon.Add(entityPlayer);
		ref var baseAttack = ref poolBaseAttacks.Add(entityPlayer);

		playerComponent.numberPlayer = 0;

		aimTimer.stateAimTime = 4;

		GameObject player = sceneData.PlayerInstance;
		var animEvents = player.GetComponent<AnimationEvents>();
		animEvents.Init();
		inputComponent.userInput = player.GetComponent<UserInput>(); // TODO: NEED IMPROVE

		runtimeData.OwnerCameraTransform = player.transform.position;
		characterComponent.characterTransform = player.transform;

		var characterSettings = player.GetComponent<CharacterSettings>();
		healthComponent.damageable = player.GetComponent<Damageable>();
		characterRigComponent.characterRigController = player.GetComponent<CharacterRigController>();
		characterComponent.characterController = player.GetComponent<CharacterController>();
		animatorComponent.animationsManager = new PlayerAnimationsManager(player.GetComponent<Animator>(),
			animEvents);

		weaponSpawnPoint.weaponSpawPoint = player.GetComponent<WeaponSpawnPoint>().WeaponPointSpawn;

		arsenal.arsenal = player.GetComponent<Arsenal>();
		arsenal.currentNumberWeapon = -1;

		characterComponent.characterSettings = characterSettings;
		dashComponent.dashTime = characterSettings.DashTime;
		dashComponent.dashSpeed = characterSettings.DashSpeed;

		player.transform.forward = staticData.GlobalForwardVector;
		
		healthComponent.maxHealth = characterSettings.GetMaxHealth();
		healthComponent.currentHealth = healthComponent.maxHealth;

		animatorComponent.animationState = new CharacterAnimationState();

		movableComponent.transform = player.transform;

		movableComponent.moveSpeed = characterSettings.GetCharacterSpeed();

		targetComponent.target = runtimeData.CursorPosition;

		arsenal.arsenal.Init(weaponSpawnPoint.weaponSpawPoint);
		arsenal.arsenal.GetMeleeWeapon().SetMeleeAttackEvent(animEvents); // TODO: need to improve

		baseAttack.baseAttack = arsenal.arsenal.GetMeleeWeapon().BaseAttack;

		movableComponent.coefSmooth = 0.3f;
		rotatableComponent.coefSmooth = 0.3f;

		enablerComponent.instance = player;
		enablerComponent.isEnabled = true;

		rangeHit.rangeHit = 1.5f;
	}
}
