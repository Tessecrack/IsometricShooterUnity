using Leopotam.EcsLite;
using UnityEngine;

public class CharacterAimingSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var entities = world.Filter<CharacterComponent>()
			.Inc<StateAttackComponent>()
			.Inc<WeaponComponent>()
			.Inc<EnablerComponent>()
			.Exc<TurretComponent>()
			.End();

		var characterComponents = world.GetPool<CharacterComponent>();
		var statesComponents = world.GetPool<StateAttackComponent>();
		var weaponComponents = world.GetPool<WeaponComponent>();
		var enablerComponents = world.GetPool<EnablerComponent>();

		foreach(var entity in entities)
		{
			ref var enabler = ref enablerComponents.Get(entity);

			if (!enabler.isEnabled)
			{ 
				continue; 
			}

			ref var characterComponent = ref characterComponents.Get(entity);
			ref var stateComponent = ref statesComponents.Get(entity);
			ref var weaponComponent = ref weaponComponents.Get(entity);

			var weaponSpawnPoint = characterComponent.characterSettings.GetPointSpawnWeapon();
			if (stateComponent.state == CharacterState.Aiming 
				&& weaponComponent.typeWeapon != TypeWeapon.MELEE)
			{
				weaponSpawnPoint.forward = characterComponent.characterTransform.forward;
			}
		}
	}
}