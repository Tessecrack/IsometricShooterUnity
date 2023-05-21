using Leopotam.EcsLite;
using System.Numerics;

public class CharacterRigSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();

		var entities = world.Filter<CharacterRigComponent>()
			.Inc<CharacterComponent>()
			.Inc<CharacterStateAttackComponent>()
			.Inc<MovableComponent>()
			.End();

		var sharedData = systems.GetShared<SharedData>();
		var cursorPosition = sharedData.RuntimeData.CursorPosition;

		var characterRigComponents = world.GetPool<CharacterRigComponent>();
		var characterComponents = world.GetPool<CharacterComponent>();
		var stateComponents = world.GetPool<CharacterStateAttackComponent>();
		var movableComponents = world.GetPool<MovableComponent>();

		foreach(var entity in entities)
		{
			ref var characterRigComponent = ref characterRigComponents.Get(entity);
			ref var characterComponent = ref characterComponents.Get(entity);
			ref var stateComponent = ref stateComponents.Get(entity);
			ref var movableComponent = ref movableComponents.Get(entity);

			if (stateComponent.characterState == CharacterState.Rest 
				&& movableComponent.velocity.magnitude == 0)
			{
				characterRigComponent.characterRigController.SetTargetHeadChestRig(cursorPosition);
			}	
			else
			{
				characterRigComponent.characterRigController.ResetHeadChesRig();
			}
		}
	}
}
