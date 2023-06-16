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
			.Exc<TurretComponent>()
			.End();
		var characterComponents = world.GetPool<CharacterComponent>();
		var statesComponents = world.GetPool<StateAttackComponent>();
		var weaponComponents = world.GetPool<WeaponComponent>();
		foreach(var entity in entities)
		{
			ref var characterComponent = ref characterComponents.Get(entity);
			if (!characterComponent.instance.activeSelf)
			{ 
				continue; 
			}
			ref var stateComponent = ref statesComponents.Get(entity);
			ref var weaponComponent = ref weaponComponents.Get(entity);

			var weaponSpawnPoint = characterComponent.characterSettings.GetPointSpawnWeapon();
			if (stateComponent.state == CharacterState.Aiming && weaponComponent.typeWeapon != TypeWeapon.MELEE)
			{
				weaponSpawnPoint.forward = characterComponent.characterTransform.forward;
			}
		}
	}
}