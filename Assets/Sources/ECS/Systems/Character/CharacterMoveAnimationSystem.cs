using Leopotam.Ecs;

public class CharacterMoveAnimationSystem : IEcsRunSystem
{
	private EcsFilter<AnimatorComponent, MovableComponent> filter;

	public void Run()
	{
		foreach(var i in filter)
		{
			ref var animatorComponent = ref filter.Get1(i);
			ref var movableComponent = ref filter.Get2(i);

			animatorComponent.animator.SetBool(AnimationParams.BOOL_RUN_NAME_PARAM, 
				movableComponent.velocity.z != 0 || movableComponent.velocity.x != 0);
			animatorComponent.animator.SetFloat(AnimationParams.FLOAT_VERTICAL_MOTION_NAME_PARAM, movableComponent.velocity.z);
			animatorComponent.animator.SetFloat(AnimationParams.FLOAT_HORIZONTAL_MOTION_NAME_PARAM, movableComponent.velocity.x);
		}
	}
}
