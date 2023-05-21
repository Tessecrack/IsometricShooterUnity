using Leopotam.EcsLite;

public class CharacterAimingSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var entities = world.Filter<CharacterComponent>()
			.Inc<CharacterStateAttackComponent>()
			.End();
		var characterComponents = world.GetPool<CharacterComponent>();
		var statesComponent = world.GetPool<CharacterStateAttackComponent>();

		foreach( var entity in entities )
		{
			ref var characterComponent = ref characterComponents.Get(entity);
			ref var stateComponent = ref statesComponent.Get(entity);

			var weaponSpawnPoint = characterComponent.characterSettings.GetPointSpawnWeapon();
			if (stateComponent.characterState == CharacterState.Aiming)
			{
				weaponSpawnPoint.forward = characterComponent.characterTransform.forward;
			}
		}
	}
}
