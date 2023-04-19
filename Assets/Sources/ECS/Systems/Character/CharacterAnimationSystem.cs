using Leopotam.Ecs;

public class CharacterAnimationSystem : IEcsRunSystem
{
	private EcsFilter<AnimatorComponent, MovableComponent, WeaponComponent> filter;

	public void Run()
	{
		foreach(var i in filter)
		{
			ref var animatorComponent = ref filter.Get1(i);
			ref var movableComponent = ref filter.Get2(i);
			ref var weaponComponent = ref filter.Get3(i);

			animatorComponent.animator.SetFloat(AnimationParams.FLOAT_TYPE_WEAPON_NAME_PARAM, (int)weaponComponent.typeWeapon);
			animatorComponent.animator.SetBool(AnimationParams.BOOL_RUN_NAME_PARAM, 
				movableComponent.velocity.z != 0 || movableComponent.velocity.x != 0);
			animatorComponent.animator.SetFloat(AnimationParams.FLOAT_VERTICAL_MOTION_NAME_PARAM, movableComponent.velocity.z);
			animatorComponent.animator.SetFloat(AnimationParams.FLOAT_HORIZONTAL_MOTION_NAME_PARAM, movableComponent.velocity.x);
		}
	}
}
