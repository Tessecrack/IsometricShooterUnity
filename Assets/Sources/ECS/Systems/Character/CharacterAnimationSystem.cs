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
			.Inc<CharacterStateComponent>()
			.End();

		var animators = world.GetPool<AnimatorComponent>();
		var movables = world.GetPool<MovableComponent>();
		var weapons = world.GetPool<WeaponComponent>();
		var attacks = world.GetPool<AttackComponent>();
		var characterStates = world.GetPool<CharacterStateComponent>();

		foreach(int entity in filter)
		{
			ref var animatorComponent = ref animators.Get(entity);
			ref var movableComponent = ref movables.Get(entity);
			ref var weaponComponent = ref weapons.Get(entity);
			ref var attackComponent = ref attacks.Get(entity);
			ref var characterStateComponent = ref characterStates.Get(entity);

			var currentState = characterStateComponent.characterState;

			animatorComponent.animationsManager.SetTypeWeapon(weaponComponent.typeWeapon);
			animatorComponent.animationsManager.SetRun(movableComponent.velocity.z != 0 || movableComponent.velocity.x != 0);
			animatorComponent.animationsManager.SetVerticalMotion(movableComponent.relativeVector.z);
			animatorComponent.animationsManager.SetHorizontalMotion(movableComponent.relativeVector.x);
			animatorComponent.animationsManager.SetAttackMode(currentState);
		}
	}
}
