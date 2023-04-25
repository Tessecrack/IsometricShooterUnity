using Leopotam.Ecs;
public class PlayerAttackSystem : IEcsRunSystem
{
	private EcsFilter<InputEventComponent, AttackComponent> filter;

	public void Run()
	{
		foreach (var i in filter)
		{
			ref var inputComponent = ref filter.Get1(i);
			ref var attackComponent = ref filter.Get2(i);

			attackComponent.isStartAttack = inputComponent.isAttack;
		}
	}
}
