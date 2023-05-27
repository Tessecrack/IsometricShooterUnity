using Leopotam.EcsLite;

public class CharacterAimingSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var entities = world.Filter<CharacterComponent>()
			.Inc<CharacterStateAttackComponent>()
			.Inc<WeaponComponent>()
			.End();
		var characterComponents = world.GetPool<CharacterComponent>();
		var statesComponents = world.GetPool<CharacterStateAttackComponent>();
		var weaponComponents = world.GetPool<WeaponComponent>();
		foreach( var entity in entities )
		{
			ref var characterComponent = ref characterComponents.Get(entity);
			ref var stateComponent = ref statesComponents.Get(entity);
			ref var weaponComponent = ref weaponComponents.Get(entity);

			var weaponSpawnPoint = characterComponent.characterSettings.GetPointSpawnWeapon();
			if (stateComponent.characterState == CharacterState.Aiming && weaponComponent.typeWeapon != TypeWeapon.MELEE)
			{
				weaponSpawnPoint.forward = characterComponent.characterTransform.forward;
			}
		}
	}
}
