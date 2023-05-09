using Leopotam.EcsLite;

public class PlayerAttackSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<InputEventComponent>()
			.Inc<AttackComponent>()
			.Inc<CharacterComponent>()
			.End();

		var inputs = world.GetPool<InputEventComponent>();
		var attacks = world.GetPool<AttackComponent>();
		var characters = world.GetPool<CharacterComponent>();

		var sharedData = systems.GetShared<SharedData>();

		foreach (int entity in filter)
		{
			ref var inputComponent = ref inputs.Get(entity);
			ref var attackComponent = ref attacks.Get(entity);
			ref var characterComponent = ref characters.Get(entity);

			attackComponent.isStartAttack = inputComponent.isStartAttack;
			attackComponent.isStopAttack = inputComponent.isStopAttack;

			attackComponent.targetPoint = sharedData.RuntimeData.CursorPosition;
			attackComponent.attackerTransform = characterComponent.currentPosition;
		}
	}
}
