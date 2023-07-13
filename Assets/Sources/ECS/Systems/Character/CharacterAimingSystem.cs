using Leopotam.EcsLite;

public class CharacterAimingSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var entities = world.Filter<CharacterComponent>()
			.Inc<StateAttackComponent>()
			.Inc<WeaponComponent>()
			.Inc<EnablerComponent>()
			.Inc<WeaponSpawnPointComponent>()
			.Exc<TurretComponent>()
			.End();

		var characterComponents = world.GetPool<CharacterComponent>();
		var statesComponents = world.GetPool<StateAttackComponent>();
		var weaponComponents = world.GetPool<WeaponComponent>();
		var enablerComponents = world.GetPool<EnablerComponent>();
		var weaponSpawnPoints = world.GetPool<WeaponSpawnPointComponent>();

		foreach(var entity in entities)
		{
			ref var enabler = ref enablerComponents.Get(entity);
			if (enabler.isEnabled == false)
			{ 
				continue; 
			}
			ref var stateComponent = ref statesComponents.Get(entity);
			if (stateComponent.state != CharacterState.Aiming)
			{
				continue;
			}
			ref var weaponComponent = ref weaponComponents.Get(entity);
			if (weaponComponent.weapon.TypeWeapon == TypeWeapon.MELEE)
			{
				continue;
			}

			if (stateComponent.state == CharacterState.Aiming 
				&& weaponComponent.weapon.TypeWeapon != TypeWeapon.MELEE)
			{
				ref var pointSpawnWeapon = ref weaponSpawnPoints.Get(entity);
				ref var characterComponent = ref characterComponents.Get(entity);
				pointSpawnWeapon.weaponSpawPoint.forward = characterComponent.characterTransform.forward;
			}
		}
	}
}