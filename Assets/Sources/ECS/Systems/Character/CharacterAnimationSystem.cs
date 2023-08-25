using Leopotam.EcsLite;

public class CharacterAnimationSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<AnimatorComponent>()
			.Inc<MovableComponent>()
			.Inc<WeaponTypeComponent>()
			.Inc<InputAttackComponent>()
			.Inc<AimStateComponent>()
			.Inc<CharacterStateComponent>()
			.Inc<EnablerComponent>()
			.Inc<VelocityComponent>()
			.End();

		var animators = world.GetPool<AnimatorComponent>();
		var movables = world.GetPool<MovableComponent>();
		var weaponTypes = world.GetPool<WeaponTypeComponent>();
		var attacks = world.GetPool<InputAttackComponent>();
		var aimStates = world.GetPool<AimStateComponent>();
		var enablers = world.GetPool<EnablerComponent>();
		var velocityComponents = world.GetPool<VelocityComponent>();
		var characterStates = world.GetPool<CharacterStateComponent>();

		foreach(int entity in filter)
		{
			ref var enablerComponent = ref enablers.Get(entity);
			if (!enablerComponent.isEnabled)
			{
				continue;
			}
			ref var animatorComponent = ref animators.Get(entity);
			ref var movableComponent = ref movables.Get(entity);
			ref var weaponType = ref weaponTypes.Get(entity);
			ref var attackComponent = ref attacks.Get(entity);
			ref var aimState = ref aimStates.Get(entity);
			ref var velocityComponent = ref velocityComponents.Get(entity);
			ref var characterState = ref characterStates.Get(entity);

			animatorComponent.animationState.CurrentTypeWeapon = weaponType.typeWeapon;
			animatorComponent.animationState.TypeAttack = attackComponent.typeAttack;
			animatorComponent.animationState.CharacterState = characterState.characterState;
			
			animatorComponent.animationState.AimState = aimState.aimState;

			animatorComponent.animationState.VerticalMoveValue = movableComponent.relativeVector.z;
			animatorComponent.animationState.HorizontalMoveValue = movableComponent.relativeVector.x;

			animatorComponent.animationState.IsAttack = attackComponent.isStartAttack;
			
			animatorComponent.animationsManager.ChangeAnimationsState(animatorComponent.animationState);
		}
	}
}
