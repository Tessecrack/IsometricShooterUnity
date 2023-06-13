using Leopotam.EcsLite;
public class CharacterAnimationSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<AnimatorComponent>()
			.Inc<MovableComponent>()
			.Inc<WeaponComponent>()
			.Inc<AttackComponent>()
			.Inc<StateAttackComponent>()
			.End();

		var animators = world.GetPool<AnimatorComponent>();
		var movables = world.GetPool<MovableComponent>();
		var weapons = world.GetPool<WeaponComponent>();
		var attacks = world.GetPool<AttackComponent>();
		var characterStates = world.GetPool<StateAttackComponent>();

		foreach(int entity in filter)
		{
			ref var animatorComponent = ref animators.Get(entity);
			ref var movableComponent = ref movables.Get(entity);
			ref var weaponComponent = ref weapons.Get(entity);
			ref var attackComponent = ref attacks.Get(entity);
			ref var characterStateComponent = ref characterStates.Get(entity);

			var currentState = characterStateComponent.state;

			animatorComponent.animationState.IsMoving = movableComponent.velocity.z != 0 || movableComponent.velocity.x != 0;
			animatorComponent.animationState.CurrentTypeWeapon = weaponComponent.typeWeapon;
			animatorComponent.animationState.VerticalMoveValue = movableComponent.relativeVector.z;
			animatorComponent.animationState.HorizontalMoveValue = movableComponent.relativeVector.x;
			animatorComponent.animationState.IsAttackState = currentState == CharacterState.Aiming;

			animatorComponent.animationsManager.ChangeAnimationsState(animatorComponent.animationState);
		}
	}
}
