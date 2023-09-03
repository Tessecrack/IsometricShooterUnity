using Leopotam.EcsLite;

public class BaseAttackSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<InputAttackComponent>()
			.Inc<BaseAttackComponent>()
			.Inc<TargetComponent>()
			.Inc<EnablerComponent>()
			.End();

		var inputAttacks = world.GetPool<InputAttackComponent>();
		var baseAttacks = world.GetPool<BaseAttackComponent>();
		var targets = world.GetPool<TargetComponent>();
		var enablers = world.GetPool<EnablerComponent>();

		foreach (int entity in filter)
		{
			ref var enabler = ref enablers.Get(entity);
			if (enabler.isEnabled == false)
			{
				continue;
			}

			ref var inputAttackComponent = ref inputAttacks.Get(entity);
			ref var baseAttack = ref baseAttacks.Get(entity);
			ref var targetComponent = ref targets.Get(entity);

			if (inputAttackComponent.isStartAttack)
			{
				baseAttack.baseAttack.SetTargetPosition(targetComponent.target);
				baseAttack.baseAttack.StartAttack();
			}
			if (inputAttackComponent.isStopAttack)
			{
				baseAttack.baseAttack.EndAttack();
			}	
		}
	}
}
