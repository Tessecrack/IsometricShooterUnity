using Leopotam.Ecs;
public class PlayerAttackSystem : IEcsRunSystem
{
	private EcsFilter<InputEventComponent, AttackComponent, CharacterComponent> filter;
	private RuntimeData runtimeData;
	public void Run()
	{
		foreach (var i in filter)
		{
			ref var inputComponent = ref filter.Get1(i);
			ref var attackComponent = ref filter.Get2(i);
			ref var characterComponent = ref filter.Get3(i);

			attackComponent.isStartAttack = inputComponent.isStartAttack;
			attackComponent.isStopAttack = inputComponent.isStopAttack;

			attackComponent.targetPoint = runtimeData.CursorPosition;
			attackComponent.attackerTransform = characterComponent.currentPosition;
		}
	}
}
